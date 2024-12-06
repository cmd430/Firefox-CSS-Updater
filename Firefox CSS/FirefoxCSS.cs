using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.Linq;

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
        private string userContentPath = null;
        private string userPath = null;
        private string CurrentVersion = "";
        private string LatestVersion = "";

        private void FirefoxCSS_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void BeginWork()
        {
            string profilesText = File.ReadAllText(Path.Combine(FirefoxDir, "profiles.ini"));
            string profile = Regex.Match(profilesText, @"Default=Profiles/([\w\d.-]+)").Groups[1].Value;

            ProfileDir = Path.Combine(ProfilesDir, profile);
            ChromeDir = Path.Combine(ProfileDir, "chrome");
            userChromePath = Path.Combine(ChromeDir, "userChrome.css");
            userContentPath = Path.Combine(ChromeDir, "userContent.css");
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
            Boolean DebugForceUpdate = false; // Use to 'update' even if on latest version

            LoadUserPrefs();

            panel_loading.Visible = false;
            label_loading.Text = "Updating ...";

            if (CurrentVersion != LatestVersion || DebugForceUpdate)
            {
                if (MessageBox.Show("Do you want to install the latest update?", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    panel_loading.Visible = true;

                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("User-Agent", "c#");
                        string userChrome = client.DownloadString("https://raw.githubusercontent.com/cmd430/FirefoxCSS/master/dist/chrome/userChrome.css");
                        string userContent = client.DownloadString("https://raw.githubusercontent.com/cmd430/FirefoxCSS/master/dist/chrome/userContent.css");
                        string userOptions = client.DownloadString("https://raw.githubusercontent.com/cmd430/FirefoxCSS/master/dist/user.js");

                        File.WriteAllText(userChromePath, userChrome);
                        File.WriteAllText(userContentPath, userContent);

                        // Make sure to keep user changes
                        if (flowLayoutPanel_userPrefs.Controls.Count > 0)
                        {
                            bool foundUserConfigurable = false;

                            using (StringReader reader = new StringReader(userOptions))
                            {
                                for (string userOption = reader.ReadLine(); userOption != null; userOption = reader.ReadLine())
                                {
                                    if (userOption == "//userChrome Prefs")
                                    {
                                        foundUserConfigurable = true;
                                        continue;
                                    }
                                    if (foundUserConfigurable == false) continue;

                                    GroupCollection groups = Regex.Match(userOption, @"user_pref\(""userChrome\.(\w+)"", (true|false)\);").Groups;

                                    string OptionName = groups[1].Value;
                                    string OptionValue = groups[2].Value;

                                    CheckBox existingPref = flowLayoutPanel_userPrefs.Controls.Find(OptionName, true).FirstOrDefault() as CheckBox;

                                    if (existingPref == null) continue;

                                    string oldValue = string.Concat("user_pref(\"userChrome.", OptionName, "\", ", OptionValue, ");");
                                    string newValue = string.Concat("user_pref(\"userChrome.", OptionName, "\", ", existingPref.Checked ? "true" : "false", ");");

                                    userOptions = userOptions.Replace(oldValue, newValue);
                                }
                            }
                        }

                        File.WriteAllText(userPath, userOptions);
                        File.WriteAllText(VersionFile, LatestVersion);

                        LoadUserPrefs();

                        CurrentVersion = LatestVersion;
                        textBox_currentVersion.Text = CurrentVersion;
                        panel_loading.Visible = false;
                    }
                }
            }
        }

        private void LoadUserPrefs()
        {
            textBox_user.Clear();
            flowLayoutPanel_userPrefs.Controls.Clear();

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
                    string OptionName = groups[1].Value;
                    string OptionValue = groups[2].Value;
                    CheckBox box = new CheckBox
                    {
                        Tag = userOption,
                        Name = OptionName,
                        Text = OptionName,
                        AutoSize = true,
                        Checked = OptionValue == "true",
                        Parent = flowLayoutPanel_userPrefs
                    };

                    box.CheckedChanged += checkBox_CheckedChanged;
                    flowLayoutPanel_userPrefs.Controls.Add(box);
                }
                textBox_user.Text = textBox_user.Text.Trim();
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GroupCollection groups = Regex.Match(checkbox.Tag.ToString(), @"user_pref\(""userChrome\.(\w+)"", (true|false)\);").Groups;

            string OptionName = groups[1].Value;
            string OptionValue = groups[2].Value;

            string oldValue = string.Concat("user_pref(\"userChrome.", OptionName, "\", ", OptionValue, ");");
            string newValue = string.Concat("user_pref(\"userChrome.", OptionName, "\", ", checkbox.Checked ? "true" : "false", ");");

            textBox_user.Text = textBox_user.Text.Replace(oldValue, newValue);
            checkbox.Tag = newValue;

            File.WriteAllText(userPath, textBox_user.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            BeginWork();
        }
    }
}
