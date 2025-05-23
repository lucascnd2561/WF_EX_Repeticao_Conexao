﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WF_EX_Repeticao
{
    public partial class Form1: Form
    {
        MySqlConnection Conexao;
        private string data_source = "datasource=localhost;username=root;passaword=;database=aulas_uc3";
        public int ?id_contato_selecionado = null;
        
        public Form1()
        {
            InitializeComponent();

           

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                if (id_contato_selecionado == null)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO Pessoa " + "( nome, email, cpf, idade)" + "values" + "(@nome, @email, @cpf, @idade)";

                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
                    cmd.Parameters.AddWithValue("@idade", txtIdade.Text);
                    MessageBox.Show("Contato Pessoa Inserido com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "Update pessoa" + "SET nome = @nome, email = @email, cpf = @cpf, idade = @idade " + "Where id = @id ";

                    cmd.Parameters.AddWithValue("@id", id_contato_selecionado);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
                    cmd.Parameters.AddWithValue("@idade", txtIdade.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contato Pessoa Atualizado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                }

                        id_contato_selecionado = null;
                        txtNome.Text = string.Empty;   
                        txtEmail.Text = string.Empty;   
                        txtCPF.Text = string.Empty;   
                        txtIdade.Text = string.Empty;

                        
                        
               




            } catch (Exception ex)
            {
                MessageBox.Show("Error" + "has occured: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string busca = "%" + txtBuscar.Text + "%";

                Conexao = new MySqlConnection(data_source);

                string bananasql = "SELECT * FROM pessoa WHERE nome LIKE @m OR telefone LIKE @x  ";


                Conexao.Open();

                MySqlCommand buscar = new MySqlCommand(bananasql, Conexao);
                buscar.Parameters.AddWithValue("@m", busca);
                buscar.Parameters.AddWithValue("@x", busca);

                MySqlDataReader reader = buscar.ExecuteReader();

                lstPessoa.Items.Clear();

                while(reader.Read())
                {
                    string[] linha =
                    {
                        reader.GetInt32(0).ToString(),  // id
                        reader.GetString(1),            // nome           
                        reader.GetString(2),            // cpf
                        reader.GetString(3),            // email

                    };
                    var linha_list_view = new ListViewItem(linha);
                    lstPessoa.Items.Add(linha_list_view);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Conexao != null) 
                Conexao.Close();
            }
            

        }
    }
}
