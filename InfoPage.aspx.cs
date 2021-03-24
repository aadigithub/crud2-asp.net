using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace sample
{
   
    public partial class InfoPage : System.Web.UI.Page
    {
        //string cs = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        SqlConnection con = new SqlConnection("Data Source=AADI;Initial Catalog=Practice_h3_infotech;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                loadrecord();

                updateButton.Enabled = false;
                
            }


        }
        protected void saveButton_Click(object sender, EventArgs e)
        {


            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into info values('" + nameTextBox.Text.ToString() + "','" + fnameTextBox.Text.ToString() + "','" + addressTextBox.Text.ToString() + "') ", con);
            cmd.ExecuteNonQuery();
            con.Close();
            //ScriptManager.RegisterStartupScript(this,this.GetType(),"script","alert('Successfully Inserted');",true);
            loadrecord();

        }
        void loadrecord()
        {

            SqlCommand cmd = new SqlCommand("select * from  info", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    updateButton.Enabled = true;
        //    int rowind = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
        //    CheckBox cb = (CheckBox)GridView1.Rows[rowind].FindControl("Chk");
        //    if (cb.Checked)
        //    {
        //        nameTextBox.Text = GridView1.Rows[rowind].Cells[1].Text;
        //        fnameTextBox.Text = GridView1.Rows[rowind].Cells[2].Text;
        //        addressTextBox.Text = GridView1.Rows[rowind].Cells[3].Text;

        //    }
        //    else
        //    {
        //        nameTextBox.Text = "";
        //        fnameTextBox.Text = "";
        //        addressTextBox.Text = "";
        //    }

        //}

        protected void updateButton_Click(object sender, EventArgs e)
        {
            //con.Open();

            //SqlCommand cmd = new SqlCommand("Insert into info values('" + nameTextBox.Text.ToString() + "','" + fnameTextBox.Text.ToString() + "','" + addressTextBox.Text.ToString() + "') ", con);
            //cmd.ExecuteNonQuery();
            //con.Close();
            ////ScriptManager.RegisterStartupScript(this,this.GetType(),"script","alert('Successfully Inserted');",true);
            //loadrecord();

            con.Open();
            //SqlCommand cmd = new SqlCommand("sp_updatedata", con);
            SqlCommand cmd = new SqlCommand("Update info set name= '" + nameTextBox.Text.ToString() + "', fname='" + fnameTextBox.Text.ToString() + "', address='" + addressTextBox.Text.ToString() + "'where id ='" + nameTextBox.Text.ToString() + "' ", con);
            cmd.ExecuteNonQuery();
            cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("name", nameTextBox.Text);
            cmd.Parameters.AddWithValue("fname", fnameTextBox.Text);
            cmd.Parameters.AddWithValue("address", addressTextBox.Text);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            loadrecord(); 
        }

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == 'Click_view')
        //    {
        //        LinkButton lnkView = (LinkButton)e.CommandSource;
        //        string dealId = lnkView.CommandArgument;
        //    }
        //}

        protected void lnkView_Click(object sender, EventArgs e)
        {
            updateButton.Enabled = true;
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string rowNumber = grdrow.Cells[1].Text;
            string dealId = grdrow.Cells[2].Text;
            string dealTitle = grdrow.Cells[3].Text;

            nameTextBox.Text = grdrow.Cells[2].Text;
            fnameTextBox.Text = grdrow.Cells[3].Text;
            addressTextBox.Text = grdrow.Cells[4].Text;


        }
    }
}

