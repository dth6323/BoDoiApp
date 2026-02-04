using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.VIIBaoDamQuanY
{
    public partial class _2CanDoi : UserControl
    {
        public _2CanDoi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(IsDataExist())
            {
                UpdateData();
            }
            else
            {
                SaveData();
            }
            NavigationService.Navigate(new _3YDinh());
        }
        private bool IsDataExist()
        {
            string sql = "SELECT COUNT(*) FROM CanDoi WHERE UserId = @UserId;";
            using (var connection = new System.Data.SQLite.SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS CanDoi (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    QYDTu TEXT NOT NULL,
    QYDDen TEXT NOT NULL,
    QYETu TEXT NOT NULL,
    QYEDen TEXT NOT NULL,
    TramYTeTu TEXT NOT NULL,
    TramYTeDen TEXT NOT NULL,
    TongTu TEXT NOT NULL,
    TongDen TEXT NOT NULL
);";
            using (var connection = new System.Data.SQLite.SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void SaveData()
        {
            string sql  = @"INSERT INTO CanDoi (UserId, QYDTu, QYDDen, QYETu, QYEDen, TramYTeTu, TramYTeDen, TongTu, TongDen) Values (@UserId, @QYDTu, @QYDDen, @QYETu, @QYEDen, @TramYTeTu, @TramYTeDen, @TongTu, @TongDen);";
            using (var connection = new System.Data.SQLite.SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);
                    command.Parameters.AddWithValue("@QYDTu", txtQYdTu.Text);
                    command.Parameters.AddWithValue("@QYDDen", txtQYdDen.Text);
                    command.Parameters.AddWithValue("@QYETu", txtDYeTu.Text);
                    command.Parameters.AddWithValue("@QYEDen", txtQYeDen.Text);
                    command.Parameters.AddWithValue("@TramYTeTu", txtYteTu.Text);
                    command.Parameters.AddWithValue("@TramYTeDen", txtYteDen.Text);
                    command.Parameters.AddWithValue("@TongTu", txtTongTu.Text);
                    command.Parameters.AddWithValue("@TongDen", txtTongDen.Text);
                    command.ExecuteNonQuery();
                }
            }
        }
        private void UpdateData()
        {
                       string sql = @"UPDATE CanDoi SET QYDTu = @QYDTu, QYDDen = @QYDDen, QYETu = @QYETu, QYEDen = @QYEDen, TramYTeTu = @TramYTeTu, TramYTeDen = @TramYTeDen, TongTu = @TongTu, TongDen = @TongDen WHERE UserId = @UserId;";
            using (var connection = new System.Data.SQLite.SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);
                    command.Parameters.AddWithValue("@QYDTu", txtQYdTu.Text);
                    command.Parameters.AddWithValue("@QYDDen", txtQYdDen.Text);
                    command.Parameters.AddWithValue("@QYETu", txtDYeTu.Text);
                    command.Parameters.AddWithValue("@QYEDen", txtQYeDen.Text);
                    command.Parameters.AddWithValue("@TramYTeTu", txtYteTu.Text);
                    command.Parameters.AddWithValue("@TramYTeDen", txtYteDen.Text);
                    command.Parameters.AddWithValue("@TongTu", txtTongTu.Text);
                    command.Parameters.AddWithValue("@TongDen", txtTongDen.Text);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void _2CanDoi_Load(object sender, EventArgs e)
        {
            CreateTable();
        }
    }
}

