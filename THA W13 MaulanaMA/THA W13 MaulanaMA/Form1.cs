using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace THA_W13_MaulanaMA
{
    public partial class Form1 : Form
    {
        MySqlConnection sqlConnect;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        MySqlDataReader reader;
        string connectionString;
        string sqlQuery;
        DataTable dtTim = new DataTable();
        DataTable dtTim2 = new DataTable();
        DataTable dtNation = new DataTable();
        DataTable dgv1 = new DataTable();
        DataTable dgv2 = new DataTable();
        DataTable dgv3 = new DataTable();
        public Form1()
        {
            InitializeComponent();
            connectionString = "server=localhost;uid=root;pwd=;database=premier_league;port=8111";//port diganti
            sqlConnect = new MySqlConnection(connectionString);
            sqlConnect.Open();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlQuery = "SELECT team_name as 'Team Name', team_id as 'Team ID' FROM team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTim);
            cmbTeam.DataSource = dtTim;
            cmbTeam.ValueMember = "Team ID";
            cmbTeam.DisplayMember = "Team Name";

            sqlQuery = "SELECT nation as 'Nation', nationality_id as 'Nationality ID' FROM nationality";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtNation);
            cmbNation.DataSource = dtNation;
            cmbNation.ValueMember = "Nationality ID";
            cmbNation.DisplayMember = "Nation";

            cmbPos.Items.Add("D");
            cmbPos.Items.Add("F");
            cmbPos.Items.Add("G");
            cmbPos.Items.Add("M");

            sqlQuery = "SELECT team_name as 'Team Name', team_id as 'Team ID' FROM team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTim2);
            cmbTeam2.DataSource = dtTim2;
            cmbTeam2.ValueMember = "Team ID";
            cmbTeam2.DisplayMember = "Team Name";
            cmbTeam3.DataSource = dtTim2;
            cmbTeam3.ValueMember = "Team ID";
            cmbTeam3.DisplayMember = "Team Name";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string id = txtID.Text;
            string name = txtName.Text;
            string height = txtHeight.Text;
            string weight = txtWeight.Text;
            string tim = cmbTeam.SelectedValue.ToString();
            string nation = cmbNation.SelectedValue.ToString();
            string pos = cmbPos.SelectedItem.ToString();
            string num = txtNumber.Text;
            string birth = dtp.Text.ToString();

            string command = $"insert into player values('{id}','{num}','{name}','{nation}','{pos}','{height}','{weight}','{birth}','{tim}',1,0)";
            try
            {
                sqlConnect.Open();
                sqlCommand = new MySqlCommand(command, sqlConnect);
                reader = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(command, ex.Message);

            }
            finally
            {
                sqlConnect.Close();
            }

            /*
            sqlQuery= $"insert into player values ('{id}','{num}','{name}','{nation}','{pos}','{height}','{weight}','{birth}','{tim}',1,0)";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlCommand.ExecuteNonQuery();*/
        }

        private void addPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
           
        }

        private void editManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            panel3.Visible = false;
        }
        string x = "";
        

        private void cmbTeam2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv1.Clear();
            sqlQuery = $"select m.manager_id,m.manager_name,t.team_name,m.birthdate, m.nationality_id, m.working from manager m left join team t on m.manager_id = t.manager_id where t.team_id = '{cmbTeam2.SelectedValue.ToString()}';";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dgv1);
            // x = dgv1.Rows[0][0].ToString();
            DGV1.DataSource = dgv1;

            dgv2.Clear();
            sqlQuery = $"select m.manager_id,m.manager_name,m.birthdate, m.nationality_id, m.working from manager m  where m.working = '0';";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dgv2);
            DGV2.DataSource = dgv2;
        }
        
        private void btnChange_Click(object sender, EventArgs e)
        {
            
            string command = $"update team set manager_id = '{idnew}' where team_id = '{cmbTeam2.SelectedValue}';";
            try
            {
                sqlConnect.Open();
                sqlCommand = new MySqlCommand(command, sqlConnect);
                reader = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(command, ex.Message);

            }
            finally
            {
                sqlConnect.Close();
            }
            command = $"update manager set working = '0' where manager_id = '{x}';";
            try
            {
                sqlConnect.Open();
                sqlCommand = new MySqlCommand(command, sqlConnect);
                reader = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(command, ex.Message);

            }
            finally
            {
                sqlConnect.Close();
            }
            command = $"update manager set working = '1' where manager_id = '{idnew}';";
            try
            {
                sqlConnect.Open();
                sqlCommand = new MySqlCommand(command, sqlConnect);
                reader = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(command, ex.Message);

            }
            finally
            {
                sqlConnect.Close();
            }
        }
        
        string idnew = "";
        
        private void DGV2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // idnew = dgv2.Rows[e.RowIndex][0].ToString();
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
        }

        private void cmbTeam3_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlQuery = $"select p.player_id,p.player_name,n.nation,p.playing_pos,p.team_number, p.height, p.weight, p.birthdate, p.status from player p,nationality n where p.team_id = '{cmbTeam3.SelectedValue.ToString()}' and n.nationality_id = p.nationality_id and p.status = '1';";
            sqlConnect = new MySqlConnection(connectionString);

            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dgv3);
            DGV3.DataSource = dgv3;
            // DGV3.Columns.Remove("player_id");
        }
        
        private void Delete_Click(object sender, EventArgs e)
        {
            
            if (dgv3.Rows.Count < 11)
            {
                MessageBox.Show("KURANG DARI 11");
            }
            else
            {
                string command = $"update player set status = '0' where player_id = '{idbaru2}';";
                try
                {
                    sqlConnect.Open();
                    sqlCommand = new MySqlCommand(command, sqlConnect);
                    reader = sqlCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(command, ex.Message);

                }
                finally
                {
                    sqlConnect.Close();
                }
                dgv3.Clear();
                sqlQuery = $"select p.player_id,p.player_name,n.nation,p.playing_pos,p.team_number, p.height, p.weight, p.birthdate, p.status from player p,nationality n where p.team_id = '{cmbTeam3.SelectedValue.ToString()}' and n.nationality_id = p.nationality_id and p.status = '1';";
                sqlConnect = new MySqlConnection(connectionString);
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dgv3);
                DGV3.DataSource = dgv3;
            }

            
        }
        string idbaru2 = "";
        
        private void DGV3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idbaru2 = dgv3.Rows[e.RowIndex][0].ToString();

        }
    }
}
