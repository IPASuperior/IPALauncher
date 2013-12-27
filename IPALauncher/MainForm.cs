/*
 * Created by SharpDevelop.
 * User: bellahwe
 * Date: 12/11/2013
 * Time: 9:53 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;
using System.Diagnostics;

namespace IPALauncher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
	    string banlocation;
	    string gamelocation;
	    string presetlocation;
	    int id = 0;
	    
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            
            toolTip1.SetToolTip(this.label14, "If this is true, then players can see each other on the map");
            toolTip1.SetToolTip(this.comboBox1, "This allows support to old Alpha 1");
            
			MessageBox.Show("Version 1.3.0.0 supports: A1 - A5" + "\n" + "\n" + "This may support other versions as well\n\nThis is still in beta, so yeah. It may have a few bugs");
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			// When the combobox1, aka version selector, is changed...
			if (comboBox1.SelectedIndex == 0)
			{
				MessageBox.Show("You have selected Alpha 1. Updating settings for support...");
				diff.Items.Clear();
				diff.Items.Add("1");
				diff.Items.Add("2");
				diff.Items.Add("3");
				diff.Items.Add("4");
				diff.Items.Add("5");
				diff.Items.Add("6");
				diff.Items.Add("7");
				diff.Items.Add("8");
				diff.Items.Add("9");
				diff.Items.Add("10");
				gmode.Items.Clear();
				gmode.Items.Add("Survival");
				gmode.Items.Add("Deathmatch");
				panel10.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
				prtNum.Enabled = true;
				svrName.Enabled = true;
				gName.Enabled = true;
				plrMax.Enabled = true;
				diff.Enabled = true;
				gmode.Enabled = true;
				button2.Enabled = true;
			}
			
			if (comboBox1.SelectedIndex == 1)
			{
				MessageBox.Show("You have selected Alpha 2 - 5. Updating settings for support...");
				diff.Items.Clear();
				diff.Items.Add("0");
				diff.Items.Add("1");
				diff.Items.Add("2");
				diff.Items.Add("3");
				diff.Items.Add("4");
				gmode.Items.Clear();
				gmode.Items.Add("Survival");
				gmode.Items.Add("Deathmatch");
				gmode.Items.Add("Zombie Horde");
				panel10.Enabled = true;
				button3.Enabled = true;
				button4.Enabled = true;
				prtNum.Enabled = true;
				svrName.Enabled = true;
				gName.Enabled = true;
				plrMax.Enabled = true;
				diff.Enabled = true;
				gmode.Enabled = true;
				button2.Enabled = true;
			}
			
			if (comboBox1.SelectedIndex == 2)
			{
				MessageBox.Show("Anything above 5 is not yet officially supported yet, but may still be compatible. \n \nI am not responsible for any corruptions/problems caused by using unsupported versions.");
				diff.Items.Clear();
				diff.Items.Add("0");
				diff.Items.Add("1");
				diff.Items.Add("2");
				diff.Items.Add("3");
				diff.Items.Add("4");
				gmode.Items.Clear();
				gmode.Items.Add("Survival");
				gmode.Items.Add("Deathmatch");
				gmode.Items.Add("Zombie Horde");
				panel10.Enabled = true;
				button3.Enabled = true;
				button4.Enabled = true;
				prtNum.Enabled = true;
				svrName.Enabled = true;
				gName.Enabled = true;
				plrMax.Enabled = true;
				diff.Enabled = true;
				gmode.Enabled = true;
				button2.Enabled = true;
			}
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			
			FolderBrowserDialog fdb = new FolderBrowserDialog();
			
			if(fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				// Sets the location
                gamelocation = fdb.SelectedPath;
                // Changed the text
                button2.Text = "-- LOCATION CHOSEN --";
                
                button1.Enabled = true;
                
                checkBox1.Enabled = true;
			}
			button5.Enabled = true;
			banme.Enabled = true;
		}
		
		
		
		
		void Button3Click(object sender, EventArgs e)
		{
			// Opens the file dialog for finding the exe
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // Sets the title of the window
            openFileDialog1.Title = "Select XML File";
            // Sets the specifics and such
            openFileDialog1.Filter = "|*.xml";

            // If something is selected...
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Sets the location
                presetlocation = openFileDialog1.FileName;
                // Changed the text
                button3.Text = "-- PRESETS IMPORTED --";
                
                
                // XML READING DONE HERE!!! //
                using (XmlReader prereader = XmlReader.Create(presetlocation))
				{
	    			while (prereader.Read())
	    			{
					if (prereader.IsStartElement())
					{
		    			switch (prereader.Name)
		    			{
						case "ServerSettings":
			    			break;
						case "property":
			    			string name = prereader["name"];
			    			string value = prereader["value"];
			    			if (name == "ServerPort")
			    			{
			    				prtNum.Text = value;
			    			}
			    			if (name == "ServerIsPublic")
			    			{
			    				if (value == "true")
			    				{
			    					pubyes.Checked = true;
			    					pubno.Checked = false;
			    				}
			    				if (value == "false")
			    				{
			    					pubyes.Checked = false;
			    					pubno.Checked = true;
			    				}
			    			}
			    			if (name == "ServerName")
			    			{
			    				svrName.Text = value;
			    			}
			    			if (name == "ServerPassword")
			    			{
			    				svrPass.Text = value;
			    			}
			    			if (name == "ServerMaxPlayerCount")
			    			{
			    				plrMax.Text = value;
			    			}
			    			if (name == "GameWorld")
			    			{
			    				if (value == "Navezgane")
			    				{
			    					mwSel.SelectedIndex = 0;
			    				}
			    				if (value == "MP Wasteland Horde")
			    				{
			    					mwSel.SelectedIndex = 1;
			    				}
			    				if (value == "MP Wasteland Skirmish")
			    				{
			    					mwSel.SelectedIndex = 2;
			    				}
			    				if (value == "MP Wasteland War")
			    				{
			    					mwSel.SelectedIndex = 3;
			    				}
			    				if (value == "MP Forest Horde")
			    				{
			    					mwSel.SelectedIndex = 4;
			    				}
			    				if (value == "MP Forest Wasteland")
			    				{
			    					mwSel.SelectedIndex = 5;
			    				}
			    			}
			    			if (name == "GameName")
			    			{
			    				gName.Text = value;
			    			}
			    			if (name == "GameDifficulty")
			    			{
			    				if (value == "0")
			    				{
			    					diff.SelectedIndex = 0;	
			    				}
			    				if (value == "1")
			    				{
			    					diff.SelectedIndex = 1;
			    				}
			    				if (value == "2")
			    				{
			    					diff.SelectedIndex = 2;
			    				}
			    				if (value == "3")
			    				{
			    					diff.SelectedIndex = 3;
			    				}
			    				if (value == "4")
			    				{
			    					diff.SelectedIndex = 4;
			    				}
			    			}
			    			if (name == "GameMode")
			    			{
			    				if (value == "GameModeSurvival")
			    				{
			    					gmode.SelectedIndex = 0;
			    				}
			    				if (value == "GameModeDeathmatch")
			    				{
			    					gmode.SelectedIndex = 1;
			    				}
			    				if (value == "GameModeZombieHorde")
			    				{
			    					gmode.SelectedIndex = 2;
			    				}
			    			}
			    			if (name == "EnemySpawning")
			    			{
			    				if (value == "true")
			    				{
			    					enyes.Checked = true;
			    				}
			    				if (value == "false")
			    				{
			    					enno.Checked = false;
			    				}
			    			}
			    			if (name == "ShowAllPlayersOnMap")
			    			{
			    				if (value == "true")
			    				{
			    					pmapyes.Checked = true;
			    				}
			    				if (value == "false")
			    				{
			    					pmapno.Checked = true;
			    				}
			    			}
			    			if (name == "BuildCreate")
			    			{
			    				if (value == "true")
			    				{
			    					cmyes.Checked = true;
			    				}
			    				if (value == "false")
			    				{
			    					cmno.Checked = true;
			    				}
			    			}
			    			if (name == "DayNightLength")
			    			{
			    				dnLength.Text = value;
			    			}
			    			if (name == "FriendlyFire")
			    			{
			    				if (value == "true")
			    				{
			    					ffyes.Checked = true;
			    				}
			    				if (value == "false")
			    				{
			    					ffno.Checked = true;
			    				}
			    			}
			    			if (name == "DayCount")
			    			{
			    				dcount.Text = value;
			    			}
			    			if (name == "FragLimit")
			    			{
			    				klimit.Text = value;
			    			}
			    			if (name == "MatchLength")
			    			{
			    				mlength.Text = value;
			    			}
			    			if (name == "RebuildMap")
			    			{
			    				if (value == "true")
			    				{
			    					rebuildyes.Checked = true;
			    				}
			    				if (value == "false")
			    				{
			    					rebuildno.Checked = true;
			    				}
			    			}
			    			if (name == "ControlPanelEnabled")
			    			{
			    				if (value == "true")
			    				{
			    					cpanelyes.Checked = true;
			    				}
			    				if (value == "false")
			    				{
			    					cpanelno.Checked = true;
			    				}
			    			}
			    			if (name == "ControlPanelPort")
			    			{
			    				cport.Text = value;
			    			}
			    			if (name == "ControlPanelPassword")
			    			{
			    				cpassword.Text = value;
			    			}
			    			if (name == "TelnetEnabled")
			    			{
			    				if (value == "true")
			    				{
			    					telyes.Checked = true;
			    				}
			    				if (value == "false")
			    				{
			    					telno.Checked = true;
			    				}
			    			}
			    			if (name == "TelnetPort")
			    			{
			    				telport.Text = value;
			    			}
			    			if (name == "DisableNAT")
			    			{
			    				if (value == "true")
			    				{
			    					natoffyes.Checked = true;
			    				}
			    				if (value == "false")
			    				{
			    					natoffno.Checked = true;
			    				}
			    			}
			    			if (name == "BanFileName")
			    			{
			    				banlocation = value;
			    			}
			    			break;
		    			}
					}
	    			}
				}
            }	
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			string presetsavelocation;
			string selworld = "";
			string gworrrld = "";
			string enemy = "";
			string map = "";
			string buildc = "";
			string ffire = "";
			string rebuild = "";
			string cpanelif = "";
			string telneten = "";
			string natdis = "";
			
			// Opens the file dialog for finding the exe
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // Sets the title of the window
            openFileDialog1.Title = "Select XML Save Location";
            // Sets the specifics and such
            openFileDialog1.Filter = "|*.xml";

            // If something is selected...
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            	button4.Text = "-- PRESET SAVED --";
            	
            	string pub = "true";
            	
            	if (pubyes.Checked == true)
            	{
            		pub = "true";
            	}
            	if (pubno.Checked == true)
            	{
            		pub = "false";
            	}
            	if (mwSel.SelectedIndex == 0)
            	{
            		selworld = "Navezgane";
            	}
            	if (mwSel.SelectedIndex == 1)
            	{
            		selworld = "MP Wasteland Horde";
            	}
            	if (mwSel.SelectedIndex == 2)
            	{
            		selworld = "MP Wasteland Skirmish";
            	}
            	if (mwSel.SelectedIndex == 3)
            	{
            		selworld = "MP Wasteland War";
            	}
            	if (mwSel.SelectedIndex == 4)
            	{
            		selworld = "MP Forest Horde";
            	}
            	if (mwSel.SelectedIndex == 5)
            	{
            		selworld = "MP Forest War";
            	}
            	if (gmode.SelectedIndex == 0)
            	{
            		gworrrld = "GameModeSurvival";
            	}
            	if (gmode.SelectedIndex == 1)
            	{
            		gworrrld = "GameModeDeathmatch";
            	}
            	if (gmode.SelectedIndex == 2)
            	{
            		gworrrld = "GameModeZombieHorde";
            	}
            	if (enno.Checked == true)
            	{
            		enemy = "false";
            	}
            	if (enyes.Checked == true)
            	{
            		enemy = "true";
            	}
            	if (pmapno.Checked == true)
            	{
            		map = "false";
            	}
            	if (pmapyes.Checked == true)
            	{
            		map = "true";
            	}
            	if (cmno.Checked == true)
            	{
            		buildc = "false";
            	}
            	if (cmyes.Checked == true)
            	{
            		buildc = "true";
            	}
            	if (ffno.Checked == true)
            	{
            		ffire = "false";
            	}
            	if (ffyes.Checked == true)
            	{
            		ffire = "true";
            	}
            	if (rebuildno.Checked == true)
            	{
            		rebuild = "false";
            	}
            	if (rebuildyes.Checked == true)
            	{
            		rebuild = "true";
            	}
            	if (cpanelno.Checked == true)
            	{
            		cpanelif = "false";
            	}
            	if (cpanelyes.Checked == true)
            	{
            		cpanelif = "true";
            	}
            	if (telno.Checked == true)
            	{
            		telneten = "false";
            	}
            	if (telyes.Checked == true)
            	{
            		telneten = "true";
            	}
            	if (natoffno.Checked == true)
            	{
            		natdis = "false";
            	}
            	if (natoffyes.Checked == true)
            	{
            		natdis = "true";
            	}
            	
                // Sets the location
                presetsavelocation = openFileDialog1.FileName;
                // Changed the text
                button4.Text = "-- SAVED --";
                
                XmlDocument doc = new XmlDocument();
        		XmlElement root = doc.CreateElement("ServerSettings");
				doc.AppendChild(root);
			
				// Create Port
        		XmlElement child = doc.CreateElement("property");
        		root.AppendChild(child);
        		XmlAttribute newAttribute = doc.CreateAttribute("name");
        		newAttribute.Value = "ServerPort";
        		XmlAttribute newAttribute2 = doc.CreateAttribute("value");
        		newAttribute2.Value = prtNum.Text;
        		child.Attributes.Append(newAttribute);
        		child.Attributes.Append(newAttribute2);
        		// Close Port
        	
        		
        		// Create Public Bool
        		XmlElement child1 = doc.CreateElement("property");
        		root.AppendChild(child1);
        		XmlAttribute newAttribute3 = doc.CreateAttribute("name");
        		newAttribute3.Value = "ServerIsPublic";
        		XmlAttribute newAttribute4 = doc.CreateAttribute("value");
        		newAttribute4.Value = pub;
        		child1.Attributes.Append(newAttribute3);
        		child1.Attributes.Append(newAttribute4);
        		// Close Public Bool

        		
        		// Create ServerName
        		XmlElement child2 = doc.CreateElement("property");
        		root.AppendChild(child2);
        		XmlAttribute name = doc.CreateAttribute("name");
        		name.Value = "ServerName";
        		XmlAttribute value = doc.CreateAttribute("value");
        		value.Value = svrName.Text;
        		child2.Attributes.Append(name);
        		child2.Attributes.Append(value);
        		// Close ServerName
        		
        		
        		// Create ServerPass
        		XmlElement child3 = doc.CreateElement("property");
        		root.AppendChild(child3);
        		XmlAttribute name1 = doc.CreateAttribute("name");
        		name1.Value = "ServerPassword";
        		XmlAttribute value1 = doc.CreateAttribute("value");
        		value1.Value = svrPass.Text;
        		child3.Attributes.Append(name1);
        		child3.Attributes.Append(value1);
        		// Close ServerPass
        		
        		
        		// Create ServerMaxPlayerCount
        		XmlElement child4 = doc.CreateElement("property");
        		root.AppendChild(child4);
        		XmlAttribute name2 = doc.CreateAttribute("name");
        		name2.Value = "ServerMaxPlayerCount";
        		XmlAttribute value2 = doc.CreateAttribute("value");
        		value2.Value = plrMax.Text;
        		child4.Attributes.Append(name2);
        		child4.Attributes.Append(value2);
        		// Close ServerMaxPlayerCount
        		
        		
        		// Create GameWorld
        		XmlElement child5 = doc.CreateElement("property");
        		root.AppendChild(child5);
        		XmlAttribute name3 = doc.CreateAttribute("name");
        		name3.Value = "GameWorld";
        		XmlAttribute value3 = doc.CreateAttribute("value");
        		value3.Value = selworld;
        		child5.Attributes.Append(name3);
        		child5.Attributes.Append(value3);
        		// Close GameWorld
        		
        		
        		// Create GameName
        		XmlElement child6 = doc.CreateElement("property");
        		root.AppendChild(child6);
        		XmlAttribute name4 = doc.CreateAttribute("name");
        		name4.Value = "GameName";
        		XmlAttribute value4 = doc.CreateAttribute("value");
        		value4.Value = gName.Text;
        		child6.Attributes.Append(name4);
        		child6.Attributes.Append(value4);
        		// Close GameName
        		
        		
        		// Create GameDifficulty
        		XmlElement child7 = doc.CreateElement("property");
        		root.AppendChild(child7);
        		XmlAttribute name5 = doc.CreateAttribute("name");
        		name5.Value = "GameName";
        		XmlAttribute value5 = doc.CreateAttribute("value");
        		value5.Value = selworld;
        		child7.Attributes.Append(name5);
        		child7.Attributes.Append(value5);
        		// Close GameDifficulty
        		
        		
        		// Create GameMode
        		XmlElement child8 = doc.CreateElement("property");
        		root.AppendChild(child8);
        		XmlAttribute name6 = doc.CreateAttribute("name");
        		name6.Value = "GameMode";
        		XmlAttribute value6 = doc.CreateAttribute("value");
        		value6.Value = gworrrld;
        		child8.Attributes.Append(name6);
        		child8.Attributes.Append(value6);
        		// Close GameMode
        		
        		
        		// Create EnemySpawning
        		XmlElement child9 = doc.CreateElement("property");
        		root.AppendChild(child9);
        		XmlAttribute name7 = doc.CreateAttribute("name");
        		name7.Value = "EnemySpawning";
        		XmlAttribute value7 = doc.CreateAttribute("value");
        		value7.Value = enemy;
        		child9.Attributes.Append(name7);
        		child9.Attributes.Append(value7);
        		// Close EnemySpawning
        		
        		
        		// Create ShowAllPlayersOnMap
        		XmlElement child10 = doc.CreateElement("property");
        		root.AppendChild(child10);
        		XmlAttribute name8 = doc.CreateAttribute("name");
        		name8.Value = "ShowAllPlayersOnMap";
        		XmlAttribute value8 = doc.CreateAttribute("value");
        		value8.Value = map;
        		child10.Attributes.Append(name8);
        		child10.Attributes.Append(value8);
        		// Close ShowAllPlayersOnMap
        		
        		
        		// Create BuildCreate
        		XmlElement child11 = doc.CreateElement("property");
        		root.AppendChild(child11);
        		XmlAttribute name9 = doc.CreateAttribute("name");
        		name9.Value = "BuildCreate";
        		XmlAttribute value9 = doc.CreateAttribute("value");
        		value9.Value = buildc;
        		child11.Attributes.Append(name9);
        		child11.Attributes.Append(value9);
        		// Close BuildCreate
        		
        		
        		// Create DayNightLength
        		XmlElement child12 = doc.CreateElement("property");
        		root.AppendChild(child12);
        		XmlAttribute name10 = doc.CreateAttribute("name");
        		name10.Value = "DayNightLength";
        		XmlAttribute value10 = doc.CreateAttribute("value");
        		value10.Value = dnLength.Text;
        		child12.Attributes.Append(name10);
        		child12.Attributes.Append(value10);
        		// Close DayNightLength
        		
        		
        		// Create FriendlyFire
        		XmlElement child13 = doc.CreateElement("property");
        		root.AppendChild(child13);
        		XmlAttribute name11 = doc.CreateAttribute("name");
        		name11.Value = "FriendlyFire";
        		XmlAttribute value11 = doc.CreateAttribute("value");
        		value11.Value = ffire;
        		child13.Attributes.Append(name11);
        		child13.Attributes.Append(value11);
        		// Close FriendlyFire
        		
        		
        		// Create DayCount
        		XmlElement child14 = doc.CreateElement("property");
        		root.AppendChild(child14);
        		XmlAttribute name12 = doc.CreateAttribute("name");
        		name12.Value = "DayCount";
        		XmlAttribute value12 = doc.CreateAttribute("value");
        		value12.Value = dcount.Text;
        		child14.Attributes.Append(name12);
        		child14.Attributes.Append(value12);
        		// Close DayCount
        		
        		
        		// Create FragLimit
        		XmlElement child15 = doc.CreateElement("property");
        		root.AppendChild(child15);
        		XmlAttribute name13 = doc.CreateAttribute("name");
        		name13.Value = "FragLimit";
        		XmlAttribute value13 = doc.CreateAttribute("value");
        		value13.Value = klimit.Text;
        		child15.Attributes.Append(name13);
        		child15.Attributes.Append(value13);
        		// Close FragLimit
        		
        		
        		// Create MatchLength
        		XmlElement child16 = doc.CreateElement("property");
        		root.AppendChild(child16);
        		XmlAttribute name14 = doc.CreateAttribute("name");
        		name14.Value = "MatchLength";
        		XmlAttribute value14 = doc.CreateAttribute("value");
        		value14.Value = mlength.Text;
        		child16.Attributes.Append(name14);
        		child16.Attributes.Append(value14);
        		// Close MatchLength
        		
        		
        		// Create RebuildMap
        		XmlElement child17 = doc.CreateElement("property");
        		root.AppendChild(child17);
        		XmlAttribute name15 = doc.CreateAttribute("name");
        		name15.Value = "RebuildMap";
        		XmlAttribute value15 = doc.CreateAttribute("value");
        		value15.Value = rebuild;
        		child17.Attributes.Append(name15);
        		child17.Attributes.Append(value15);
        		// Close RebuildMap
        		
        		
        		// Create ControlPanelEnabled
        		XmlElement child18 = doc.CreateElement("property");
        		root.AppendChild(child18);
        		XmlAttribute name16 = doc.CreateAttribute("name");
        		name16.Value = "ControlPanelEnabled";
        		XmlAttribute value16 = doc.CreateAttribute("value");
        		value16.Value = cpanelif;
        		child18.Attributes.Append(name16);
        		child18.Attributes.Append(value16);
        		// Close ControlPanelEnabled
        		
        		
        		// Create ControlPanelPort
        		XmlElement child19 = doc.CreateElement("property");
        		root.AppendChild(child19);
        		XmlAttribute name17 = doc.CreateAttribute("name");
        		name17.Value = "ControlPanelPort";
        		XmlAttribute value17 = doc.CreateAttribute("value");
        		value17.Value = cport.Text;
        		child19.Attributes.Append(name17);
        		child19.Attributes.Append(value17);
        		// Close ControlPanelPort
        		
        		
        		// Create ControlPanelPassword
        		XmlElement child20 = doc.CreateElement("property");
        		root.AppendChild(child20);
        		XmlAttribute name18 = doc.CreateAttribute("name");
        		name18.Value = "ControlPanelPassword";
        		XmlAttribute value18 = doc.CreateAttribute("value");
        		value18.Value = cpassword.Text;
        		child20.Attributes.Append(name18);
        		child20.Attributes.Append(value18);
        		// Close ControlPanelPassword
        		
        		
        		// Create TelnetEnabled
        		XmlElement child21 = doc.CreateElement("property");
        		root.AppendChild(child21);
        		XmlAttribute name19 = doc.CreateAttribute("name");
        		name19.Value = "TelnetEnabled";
        		XmlAttribute value19 = doc.CreateAttribute("value");
        		value19.Value = telneten;
        		child21.Attributes.Append(name19);
        		child21.Attributes.Append(value19);
        		// Close TelnetEnabled
        		
        		
        		// Create TelnetEnabled
        		XmlElement child22 = doc.CreateElement("property");
        		root.AppendChild(child22);
        		XmlAttribute name20 = doc.CreateAttribute("name");
        		name20.Value = "TelnetEnabled";
        		XmlAttribute value20 = doc.CreateAttribute("value");
        		value20.Value = telneten;
        		child22.Attributes.Append(name20);
        		child22.Attributes.Append(value20);
        		// Close TelnetEnabled
        		
        		
        		// Create TelnetPort
        		XmlElement child23 = doc.CreateElement("property");
        		root.AppendChild(child23);
        		XmlAttribute name21 = doc.CreateAttribute("name");
        		name21.Value = "TelnetPort";
        		XmlAttribute value21 = doc.CreateAttribute("value");
        		value21.Value = telport.Text;
        		child23.Attributes.Append(name21);
        		child23.Attributes.Append(value21);
        		// Close TelnetPort
        		
        		
        		// Create DisableNAT
        		XmlElement child24 = doc.CreateElement("property");
        		root.AppendChild(child24);
        		XmlAttribute name22 = doc.CreateAttribute("name");
        		name22.Value = "DisableNAT";
        		XmlAttribute value22 = doc.CreateAttribute("value");
        		value22.Value = natdis;
        		child24.Attributes.Append(name22);
        		child24.Attributes.Append(value22);
        		// Close DisableNAT
        		
        		
        		// Create BanFileName
        		XmlElement child25 = doc.CreateElement("property");
        		root.AppendChild(child25);
        		XmlAttribute name23 = doc.CreateAttribute("name");
        		name23.Value = "BanFileName";
        		XmlAttribute value23 = doc.CreateAttribute("value");
        		value23.Value = banlocation;
        		child25.Attributes.Append(name23);
        		child25.Attributes.Append(value23);
        		// Close BanFileName
        		
        		
        		doc.Save(presetsavelocation);
            }
		}
		
		void CpanelyesCheckedChanged(object sender, EventArgs e)
		{
			cport.Enabled = true;
			cpassword.Enabled = true;
		}
		
		void CpanelnoCheckedChanged(object sender, EventArgs e)
		{
			cport.Enabled = false;
			cpassword.Enabled = false;
		}
		
		void TelyesCheckedChanged(object sender, EventArgs e)
		{
			telport.Enabled = true;
		}
		
		void TelnoCheckedChanged(object sender, EventArgs e)
		{
			telport.Enabled = false;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			if (comboBox1.SelectedIndex == 0)
			{
				var process = new Process();
            	process.StartInfo.Arguments = " -quit -batchmode -nographics -port=" + prtNum.Text + " -maxplayers=" + plrMax.Text + " -gamemode=" + gmode.Text + " -difficulty=" + diff.Text + " -world=Navezgane -name=" + gName + " -dedicated";
    			process.StartInfo.FileName = gamelocation + @"\7DaysToDie";
    			process.Start();
   				id = process.Id;
			}
			
			if (comboBox1.SelectedIndex != 0)
			{
				var process = new Process();
				process.StartInfo.Arguments = " -quit -batchmode -nographics -configfile=serverconfig.xml -dedicated";
				process.StartInfo.FileName = gamelocation + @"\7DaysToDie";
				process.Start();
				id = process.Id;
			}

            if (checkBox1.Checked == true)
            {
                bakup.Enabled = true;
            }

			ram.Enabled = true;
            button7.Enabled = true;
			button6.Enabled = true;
		}
		
		void RamTick(object sender, EventArgs e)
		{
			long mem = 0;
			
			try
            {
                Process localById = System.Diagnostics.Process.GetProcessById(id);
                mem = localById.WorkingSet64 / 1048576;
                label31.Text = "RAM Usage: " + mem + "MB";
            }

            catch (Exception)
            {
                label31.Text = "RAM Usage: 0MB";
                ram.Enabled = false;
            }			
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			string banmee = "";
			
			if (banme.Text != null)
			{
				banmee = banme.Text;
				string bannnnn = gamelocation + "/ban.txt";
				
				File.AppendAllText(bannnnn, banmee + Environment.NewLine);
				
				listBox1.Items.Add(banmee + " is banned");
			}
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			Process p = Process.GetProcessById(id);
			p.Kill();
            bakup.Enabled = false;
			button6.Enabled = false;
		}
        private void PUTTY()
        {
            // This code exracts and launches putty ((MIGRATED FROM MY ORIGINAL LAUNCHER))
            // Now this shuts down the server safely

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string[] arrResources = currentAssembly.GetManifestResourceNames();
            foreach (string resourceName in arrResources)
            {
                if (resourceName.EndsWith(".exe"))
                {
                    // What to save it as
                    string saveAsName = "putty.exe";
                    FileInfo fileInfoOutputFile = new FileInfo(saveAsName);
                    if (fileInfoOutputFile.Exists)
                    {
                        // Deletes file if exists
                        fileInfoOutputFile.Delete();
                    }
                    FileStream streamToOutputFile = fileInfoOutputFile.OpenWrite();
                    Stream streamToResourceFile = currentAssembly.GetManifestResourceStream(resourceName);

                    // Is the EXACT byte size of putty.exe (needs to recreate byte by byte)
                    const int size = 495616;
                    byte[] bytes = new byte[495616];
                    int numBytes;
                    while ((numBytes = streamToResourceFile.Read(bytes, 0, size)) > 0)
                    {
                        streamToOutputFile.Write(bytes, 0, numBytes);
                    }

                    streamToOutputFile.Close();
                    streamToResourceFile.Close();
                }
            }




            // Launches putty.exe with variables entered
            var startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var puttyPath = System.IO.Path.Combine(startupPath, "putty.exe");
            System.Diagnostics.Process.Start(puttyPath, " telnet://localhost/");

        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            PUTTY();
            bakup.Enabled = false;
            button7.Enabled = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Deletes putty when complete
            var startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var puttyPath = System.IO.Path.Combine(startupPath, "putty.exe");
            File.Delete(puttyPath);
        }

        private void BACKUP()
        {
            // Also ripped from my origianl program. (Source 'may' be available)
            // This is the time setup (for organization)
            string backtime = DateTime.Now.ToString("hh.mm.ss tt");

            // This is what all the folders it's going to find/make are located
            string folder = gamelocation + @"\Data\Worlds\Navezgane\Saves\" + mwSel;
            string folder2 = gamelocation + @"\Data\Worlds\Navezgane\Saves\" + mwSel + @"\Player\";
            string folder3 = gamelocation + @"\Data\Worlds\Navezgane\Saves\" + mwSel + @"\Region\";
            string output4 = gamelocation + @"\Backups\" + backtime + @"\";
            string output = gamelocation + @"\Backups\" + backtime + @"\" + mwSel;
            string output2 = gamelocation + @"\Backups\" + backtime + @"\" + mwSel + @"\Player\";
            string output3 = gamelocation + @"\Backups\" + backtime + @"\" + mwSel + @"\Region\";


            // If the output directory exists...
            if (Directory.Exists(output))
            {
                // Attempts
                try
                {
                    // Deletes the directory
                    Directory.Delete(output, true);
                }
                // If error....
                catch (IOException)
                {
                    // Waits and tries again
                    System.Threading.Thread.Sleep(0);
                    Directory.Delete(output, true);
                }
            }

            // Creates the directories
            Directory.CreateDirectory(output4);
            Directory.CreateDirectory(output2);
            Directory.CreateDirectory(output3);
            Directory.CreateDirectory(output);

            // This copies all of the files and such into the proper directory
            foreach (var file in Directory.GetFiles(folder)) File.Copy(file, Path.Combine(output, Path.GetFileName(file)));

            foreach (var file in Directory.GetFiles(folder2)) File.Copy(file, Path.Combine(output2, Path.GetFileName(file)));

            foreach (var file in Directory.GetFiles(folder3)) File.Copy(file, Path.Combine(output3, Path.GetFileName(file)));
        }

        private void bakup_Tick(object sender, EventArgs e)
        {
            BACKUP();
        }
		
		void BanmeKeyDown(object sender, KeyEventArgs e)
		{
			string banmee = "";
			
			if(e.KeyData == Keys.Enter) 
        	{
            		if (banme.Text != null)
				{
					banmee = banme.Text;
					string bannnnn = gamelocation + "/ban.txt";
				
					File.AppendAllText(bannnnn, banmee + Environment.NewLine);
				
					listBox1.Items.Add(banmee + " is banned");
				}
        	}
		}
	}
}
