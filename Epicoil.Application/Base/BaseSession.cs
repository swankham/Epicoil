using Epicoil.Library.Models;
using Epicor.Mfg.Core;
using System;
using System.Configuration;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Epicoil.Appl
{
    public class BaseSession : Form
    {
        public static SessionInfo epiSession;
        public string AppServerURL;

        public BaseSession()
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "th-TH")
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            }
            epiSession = new SessionInfo();

            //Set default theme.
            initail.UseTheme(this);
        }

        public SessionInfo SessionIdentify(string userName, string userPassword)
        {
            try
            {
                //Get AppServer URL from App.config file.
                AppServerURL = ConfigurationManager.ConnectionStrings["EpicorAppServer"].ConnectionString;
                Epicor.Mfg.Core.Session curr = new Session(userName, userPassword, AppServerURL, Session.LicenseType.Default);

                if (curr.IsValidSession(curr.SessionID, curr.UserID))
                {
                    epiSession.AppServer = AppServerURL;
                    epiSession.CompanyID = curr.CompanyID;
                    epiSession.CompanyName = curr.CompanyName;
                    epiSession.PlantID = curr.PlantID;
                    epiSession.PlantName = curr.PlantName;
                    epiSession.UserID = curr.UserID;
                    epiSession.UserName = curr.UserName;
                    epiSession.SessionID = curr.SessionID;
                    epiSession.UserEmail = curr.UserEmail;
                    epiSession.Client = curr.Client.ToString();
                    epiSession.UserPassword = userPassword;

                    curr.Dispose();
                    return epiSession;
                }
                else
                {
                    MessageBox.Show("Error: Get session from Epicor is invalid." + Environment.NewLine + "Please contact administrator.", "Error", MessageBoxButtons.OK);
                    epiSession.SessionID = "x";
                    return epiSession;
                }
            }
            catch (Exception ex)
            {
                epiSession.SessionID = "x";
                MessageBox.Show("Error: " + ex.Message + Environment.NewLine + "Please contact administrator.", "Error", MessageBoxButtons.OK);
                return epiSession;
            }
        }

        public void ShowForm(string FormName)
        {
            try
            {
                Assembly asm = Assembly.GetEntryAssembly();
                string path = "Epicoil.Appl.Presentations";
                string formname = path + "." + FormName;
                Type formtype = asm.GetType(formname);
                Form f = (Form)Activator.CreateInstance(formtype, new object[] { epiSession, null }) as Form;
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form path not valid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}