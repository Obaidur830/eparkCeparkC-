using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Messaging;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace epark
{
    internal class MainClass
    {
        //public static readonly string con_string = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Documents\\eparkDb.mdf;Integrated Security=True;Connect Timeout=30;";

        public static readonly string con_string = "Data Source = (Local)\\sqlexpress01;Initial Catalog = eparkDb; Integrated Security = True;";
        
        public static SqlConnection sqlCon = new SqlConnection(con_string);

        public static bool isValidUser(string userName, string userPass)
        {
            bool isValid = false;
            string qry = @"Select * from users where uUserName= '" + userName + "' and uPass = '" + userPass + "' ";
            SqlCommand cmd = new SqlCommand(qry, sqlCon);
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["uUserName"].ToString();
                if (dt.Rows[0]["uImage"] != DBNull.Value)
                {
                    Byte[] imageArray = (byte[])dt.Rows[0]["uImage"];
                    byte[] imageByteArray = imageArray;
                    IMG = Image.FromStream(new MemoryStream(imageByteArray));

                }

            }
            return isValid;
        }

        public static void StopBuffering(Panel ctr, bool doubleBuffer)
        {
            try
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null, ctr, new object[] { doubleBuffer });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static string user;
        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        public static Image img;
        public static Image IMG
        {
            get { return img; }
            private set { img = value; }
        }

        public static int SQ1(string qry, Hashtable ht)
        {
            int res = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(qry, sqlCon);
                cmd.CommandType = CommandType.Text;
                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (sqlCon.State == ConnectionState.Closed) { sqlCon.Open(); }
                res = cmd.ExecuteNonQuery();
                if (sqlCon.State == ConnectionState.Open) { sqlCon.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sqlCon.Close();
            }
            return res;
        }
        public static void Loaddata(string qry, DataGridView gv, ListBox lb)
        {
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);
            try
            {
                SqlCommand cmd = new SqlCommand(qry, sqlCon);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                
                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string column1 = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[column1].DataPropertyName = dt.Columns[i].ToString();
                   // Console.WriteLine(column1,  "=", dt.Columns[i].ToString());

                }
                gv.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sqlCon.Close();
            }
        }
        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;
            foreach (DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }
        public static void BlurBackground(Form Model)
        {
            Form Background = new Form();
            using (Model)
            {
                Background.StartPosition = FormStartPosition.Manual;
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;
                Background.BackColor = Color.Black;
                Background.Size = mainForm.Instance.Size;
                Background.Location = mainForm.Instance.Location;
                Background.ShowInTaskbar = false;
                Background.Show();
                Model.Owner = Background;
                Model.ShowDialog(Background);
                Background.Dispose();
            }
        }

        public static void CBFill(string qry, ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(qry, sqlCon);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }

        public static bool validation(Form f)
        {
            bool isValid = false;
            int count = 0;
            foreach(Control c in f.Controls)
            {
                if(Convert.ToString(c.Tag) != "" && Convert.ToString(c.Tag) != null)
                {
                    if(c is Guna.UI2.WinForms.Guna2TextBox)
                    {
                        Guna.UI2.WinForms.Guna2TextBox t = (Guna.UI2.WinForms.Guna2TextBox)c;
                        if(t.Text.Trim() == "")
                        {
                            t.BorderColor = Color.Red;
                            t.FocusedState.BorderColor = Color.Red;
                            t.HoverState.BorderColor = Color.Red;
                            count++;
                        }
                        else
                        {
                            t.BorderColor = Color.FromArgb(213, 218, 223);
                            t.FocusedState.BorderColor = Color.FromArgb(95, 61, 204);
                            t.HoverState.BorderColor = Color.FromArgb(95, 61, 204);
                        }
                    }
                    if (c is Guna.UI2.WinForms.Guna2ComboBox)
                    {
                        Guna.UI2.WinForms.Guna2ComboBox t = (Guna.UI2.WinForms.Guna2ComboBox)c;
                        if (t.SelectedIndex == -1)
                        {
                            t.BorderColor = Color.Red;
                            t.FocusedState.BorderColor = Color.Red;
                            t.HoverState.BorderColor = Color.Red;
                            count++;
                        }
                        else
                        {
                            t.BorderColor = Color.FromArgb(213, 218, 223);
                            t.FocusedState.BorderColor = Color.FromArgb(95, 61, 204);
                            t.HoverState.BorderColor = Color.FromArgb(95, 61, 204);
                        }
                    }
                }
                if (count == 0)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        public static bool isValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex("^(?:\\+88|88)?(01[3-9]\\d{8})$");
            Match match=regex.Match(phoneNumber);
            if (!match.Success)
            {
                return false;
            }

            return true;
        }

        public static bool isValidUsername(string userName)
        {
            bool isValid = true;
            string qry = @"Select * from users where uUserName= '" + userName + "' ";
            SqlCommand cmd = new SqlCommand(qry, sqlCon);
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                isValid = false;
                
            }
            return isValid;
        }

        public static bool isValidPassword(string password)
        {
            bool isValid = true;
            if(password.Length< 5)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
