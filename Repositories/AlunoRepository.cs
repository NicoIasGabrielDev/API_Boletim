using APIBoletim.Interfaces;
using APIBoletim.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIBoletim.Context;
using System.Data.SqlClient;

namespace APIBoletim.Repositories
{
    public class AlunoRepository : IAluno

       

    {
        //Chamamos a clase do Context
        BoletimContext conexao = new BoletimContext();

        //Chamando o objeto que executará e receberá os comandos do banco
        SqlCommand cmd = new SqlCommand();
        public Aluno Alterar(int id , Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "UPDATE Aluno SET " +
                "Nome  = @nome, " +
                "RA    = @ra, " +
                "Idade = @idade WHERE IdAluno = @Id ";
            
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            cmd.Parameters.AddWithValue("@id", id);

            // Será este comando o responsável por injetar os dados no banco efetivamente
            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return a;
        }

            public Aluno BuscarporId(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM ALUNO WHERE IdAluno = @id ";
            //Fazendo a junção das variáveis para identificação
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = cmd.ExecuteReader();

            Aluno a = new Aluno();
            while(dados.Read())
            {
                a.IdAluno = Convert.ToInt32(dados.GetValue(0));
                a.Nome = dados.GetValue(1).ToString();
                a.RA = dados.GetValue(2).ToString();
                a.Idade = Convert.ToInt32(dados.GetValue(3));
            };

            conexao.Desconectar();

            return a;
        }

        public Aluno Cadastrar(Aluno a)
            {
                cmd.Connection = conexao.Conectar();

                cmd.CommandText =
                    "INSERT INTO Aluno (Nome, RA, Idade) " +
                    "VALUES" +
                    "(@nome, @ra, @idade)";
                cmd.Parameters.AddWithValue("@nome", a.Nome);
                cmd.Parameters.AddWithValue("@ra", a.RA);
                cmd.Parameters.AddWithValue("@idade", a.Idade);

                // Será este comando o responsável por injetar os dados no banco efetivamente
                cmd.ExecuteNonQuery();

                conexao.Desconectar();
                return a;
            }

        public void Excluir(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "DELETE FROM Aluno WHERE IdAluno = @id ";
            cmd.Parameters.AddWithValue("@id", id);
            

            // Será este comando o responsável por injetar os dados no banco efetivamente
            cmd.ExecuteNonQuery();

            conexao.Desconectar();
        }

        public List<Aluno> LerTodos()
        {
            //Abrir conexão
            cmd.Connection = conexao.Conectar();

            //Estruturar Consulta
            cmd.CommandText = "SELECT * FROM Aluno";
            SqlDataReader dados = cmd.ExecuteReader();

            //Criando lista de alunos
            List<Aluno> alunos = new List<Aluno>();
            while(dados.Read())
            {
                alunos.Add(
                    new Aluno()
                    {
                        IdAluno = Convert.ToInt32(dados.GetValue(0)),
                        Nome = dados.GetValue(1).ToString(),
                        RA = dados.GetValue(2).ToString(),
                        Idade = Convert.ToInt32(dados.GetValue(3))
                    }); 
            }

            //Fechar conexão
            conexao.Desconectar();

            return alunos;
        }
    }
}
