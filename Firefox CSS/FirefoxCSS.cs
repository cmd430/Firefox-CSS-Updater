using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;

namespace Firefox_CSS
{
    public partial class FirefoxCSS : Form
    {
        public FirefoxCSS()
        {
            InitializeComponent();
        }

        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string FirefoxDir = Path.Combine(AppData, "Mozilla", "Firefox");
        private static readonly string ProfilesDir = Path.Combine(FirefoxDir, "Profiles");
        private static readonly string VersionFile = Path.Combine(FirefoxDir, "css.version");
       
        private string ProfileDir = null;
        private string ChromeDir = null;
        private string userChromePath = null;
        private string userPath = null;
        private string CurrentVersion = "";
        private string LatestVersion = "";

        private void Form1_Shown(object sender, EventArgs e)
        {
            string profilesText = File.ReadAllText(Path.Combine(FirefoxDir, "profiles.ini"));
            string profile = Regex.Match(profilesText, @"Default=Profiles/([\w\d.-]+)").Groups[1].Value;

            ProfileDir = Path.Combine(ProfilesDir, profile);
            ChromeDir = Path.Combine(ProfileDir, "chrome");
            userChromePath = Path.Combine(ChromeDir, "userChrome.css");
            userPath = Path.Combine(ProfileDir, "user.js");

            if (!Directory.Exists(ChromeDir)) Directory.CreateDirectory(ChromeDir);
            if (File.Exists(VersionFile)) CurrentVersion = File.ReadAllText(VersionFile);

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "c#");
                string GitHubJson = client.DownloadString("https://api.github.com/repos/cmd430/FirefoxCSS/commits/master");
                LatestVersion = Regex.Match(GitHubJson, @"""sha"":""(.{40})"",").Groups[1].Value;
            }

            textBox_currentVersion.Text = CurrentVersion;
            textBox_latestVersion.Text = LatestVersion;

            if (CurrentVersion != LatestVersion)
            {
                if (MessageBox.Show("Do you want to install the latest update?", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("User-Agent", "c#");
                        string userChrome = client.DownloadString("https://raw.githubusercontent.com/cmd430/FirefoxCSS/master/dist/chrome/userChrome.css");
                        string user = client.DownloadString("https://raw.githubusercontent.com/cmd430/FirefoxCSS/master/dist/user.js");

                        File.WriteAllText(userChromePath, userChrome);
                        File.WriteAllText(userPath, user);
                        File.WriteAllText(VersionFile, LatestVersion);

                        CurrentVersion = LatestVersion;
                        textBox_currentVersion.Text = CurrentVersion;
                    }
                }
            }

            if (File.Exists(userPath))
            {
                IEnumerable<string> userOptions = File.ReadLines(userPath);
                bool foundUserConfigurable = false;

                foreach (string userOption in userOptions)
                {
                    textBox_user.AppendText(userOption);
                    textBox_user.AppendText("\r\n");

                    if (userOption == "//userChrome Prefs")
                    {
                        foundUserConfigurable = true;
                        continue;
                    }

                    if (foundUserConfigurable == false) continue;

                    GroupCollection groups = Regex.Match(userOption, @"user_pref\(""userChrome\.(\w+)"", (true|false)\);").Groups;
                    CheckBox box = new CheckBox
                    {
                        Tag = userOption,
                        Text = groups[1].Value,
                        AutoSize = true,
                        Checked = groups[2].Value == "true" ? true : false,
                        Parent = flowLayoutPanel1
                    };

                    box.CheckedChanged += checkBox_CheckedChanged;
                    flowLayoutPanel1.Controls.Add(box);
                }
                textBox_user.Text = textBox_user.Text.Trim();
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GroupCollection groups = Regex.Match(checkbox.Tag.ToString(), @"user_pref\(""userChrome\.(\w+)"", (true|false)\);").Groups;

            string oldValue = String.Concat("user_pref(\"userChrome.", groups[1].Value, "\", ", groups[2].Value, ");");
            string newValue = String.Concat("user_pref(\"userChrome.", groups[1].Value, "\", ", checkbox.Checked ? "true" : "false", ");");

            textBox_user.Text = textBox_user.Text.Replace(oldValue, newValue);
            checkbox.Tag = newValue;

            File.WriteAllText(userPath, textBox_user.Text);
        }
    }
}
