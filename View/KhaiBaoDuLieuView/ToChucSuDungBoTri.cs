using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class ToChucSuDungBoTri : UserControl
    {
        public ToChucSuDungBoTri()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBoxSuDung.Text == string.Empty || textboxToChuc.Text == string.Empty)
            {
                MessageBox.Show("Bạn có chắc muốn rồi đi?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void ToChucSuDungBoTri_Load(object sender, EventArgs e)
        {
            CreateTable();
            if(IsDataExist(Constants.CURRENT_USER_ID_VALUE))
            {
                LoadData(Constants.CURRENT_USER_ID_VALUE);
            }
        }

        private bool IsDataExist(string userId)
        {
            try
            {
                string sql = @"SELECT COUNT(*) 
                               FROM ToChucSuDungBoTri 
                               WHERE UserId = @UserId;";
                using (var connection = new System.Data.SQLite.SQLiteConnection(Resources.Constants.CONNECTION_STRING))
                {
                    connection.Open();
                    var command = new System.Data.SQLite.SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error (IsDataExist): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void CreateTable()
        {
            try
            {
                string sql = @"CREATE TABLE IF NOT EXISTS ToChucSuDungBoTri (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                UserId TEXT NOT NULL,
                                ToChuc TEXT NOT NULL,
                                BoTri TEXT NOT NULL
                            );";
                using (var connection = new System.Data.SQLite.SQLiteConnection(Resources.Constants.CONNECTION_STRING))
                {
                    connection.Open();
                    var command = new System.Data.SQLite.SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error (CreateTable): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddData(string userId, string toChuc, string boTri)
        {
            try
            {
                string sql = @"INSERT INTO ToChucSuDungBoTri (UserId, ToChuc, BoTri) 
                               VALUES (@UserId, @ToChuc, @BoTri);";
                using (var connection = new System.Data.SQLite.SQLiteConnection(Resources.Constants.CONNECTION_STRING))
                {
                    connection.Open();
                    var command = new System.Data.SQLite.SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ToChuc", toChuc);
                    command.Parameters.AddWithValue("@BoTri", boTri);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error (AddData): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadData(string userId)
        {
            try
            {
                string sql = @"SELECT ToChuc, BoTri 
                               FROM ToChucSuDungBoTri 
                               WHERE UserId = @UserId;";
                using (var connection = new System.Data.SQLite.SQLiteConnection(Resources.Constants.CONNECTION_STRING))
                {
                    connection.Open();
                    var command = new System.Data.SQLite.SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textboxToChuc.Text = reader["ToChuc"]?.ToString();
                            textBoxSuDung.Text = reader["BoTri"]?.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error (LoadData): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateData(string userId, string toChuc, string boTri)
        {
            try
            {
                string sql = @"UPDATE ToChucSuDungBoTri 
                               SET ToChuc = @ToChuc, BoTri = @BoTri 
                               WHERE UserId = @UserId;";
                using (var connection = new System.Data.SQLite.SQLiteConnection(Resources.Constants.CONNECTION_STRING))
                {
                    connection.Open();
                    var command = new System.Data.SQLite.SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@ToChuc", toChuc);
                    command.Parameters.AddWithValue("@BoTri", boTri);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error (UpdateData): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(IsDataExist(Constants.CURRENT_USER_ID_VALUE))
            {
                UpdateData(Constants.CURRENT_USER_ID_VALUE, textboxToChuc.Text, textBoxSuDung.Text);
                return;
            }
            AddData(Constants.CURRENT_USER_ID_VALUE, textboxToChuc.Text, textBoxSuDung.Text);

        }
    }

}