﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
//using System.Reflection;
//using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using rand9er;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Runtime.CompilerServices;
//using System.Security.Cryptography;
//using System.Runtime.Serialization.Formatters.Binary;

namespace rand9er
{
   
    public partial class rand9er : Form
    {
        public rand9er()
        {
            InitializeComponent();
        }

        //init//
        public static string data_str = "", pswap, binloc, set, seed = "42", tb_flText, textBoxSeed;
        public static int data_int = 0, counter, randl = 23, items = 236;
        public static int weapa = 0, weapb = 85, armleta = 88, armletb = 112, helma = 112;
        public static int helmb = 148, armora = 148, armorb = 192, acca = 192, accb = 224;
        public static byte[] ba_p0data2, ba_p0data7;
        public static char[] separators = new char[] { ';', ';' };
        public static int[] data_arr, a_empty = { 0 };
        public static string[] medicShops, a_stockShopItems, a_synthdata, a_stockSynthesis, a_statdata, a_stockBaseStats, a_equipdata, a_stockDefaultEquipment, a_itemdata, a_stockItems, a_gemdata, a_stockAbilityGems, a_comboSafe = new string[32];
        public static int[][] aa_medicItems, aa_shopItems;
        public static int[] a_shopItems = new int[] { 16, 16, 9, 13, 25, 18, 28, 13, 14, 32, 14, 32, 29, 21, 22, 25, 21, 30, 21, 30, 6, 12, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //default
        public static int[] a_shopItems_1safe = new int[] { 16, 16, 9, 13, 25, 18, 28, 13, 14, 32, 14, 32, 29, 21, 22, 25, 21, 30, 21, 30, 6, 12, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //filled zeros so no exceptions
        public static int[] a_shopItems_2max = new int[] { 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] a_shopItems_2maxm = new int[] { 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
        public static int[] a_shopItems_3rand = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static bool c_mogBool, c_safeBool, c_maxBool, c_randomBool, c_medicshopsBool, c_medicitemsBool, c_medicshopsEn, c_randomEn, c_maxEn, c_requireBool, c_resultBool, c_pricesBool, c_all_eEn, c_all_eBool, c_random_eEn, c_random_eBool, c_main_eBool, c_abilitygemsBool, c_defaultBool, c_basestatsBool;
        public static bool pathFound = false, pathSeedFolder = false;
        public static string pathSeed = "";

        List<string> memoriaINI;
        List<string> modNames = new List<string>();
        string memoriaFolderNamesLine;
        List<string> csvNeeded = new List<string>
            {
                "\\StreamingAssets\\Data\\Items\\Items.csv",
                "\\StreamingAssets\\Data\\Items\\ShopItems.csv",
                "\\StreamingAssets\\Data\\Items\\Synthesis.csv",
                "\\StreamingAssets\\Data\\Characters\\BaseStats.csv",
                "\\StreamingAssets\\Data\\Characters\\DefaultEquipment.csv",
                "\\StreamingAssets\\Data\\Characters\\AbilityGems.csv",
                "\\StreamingAssets\\p0data2.bin",
                "\\StreamingAssets\\p0data7.bin"
            };

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_seed.Text = seed;
            tb_fl.Text = path_search(pswap);
        }
        
        //      MAIN Program Execution        //
        //      MAIN Program Execution        //

        private void button_rand_Click(object sender, EventArgs e)
        {
            // ffix location, run seed and create folder
            if (pathFound)
            {
                if (MessageBox.Show("This will save all Randoms a new Mod folder", "Cormfirm Write Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    ExecuteProgram();
                }
            }
            else
            {
                if (MessageBox.Show("You need to specify the FFIX install path", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    Manual_search();
                }
            }
        }

        public void ExecuteProgram()
        {
            //Gaurentee Seed
            if (textBox_seed.Text.Length == 0)
            {
                seed = "42";
                textBox_seed.Text = seed;
            }
            seed = textBox_seed.Text;

            // Run Seed Code if needed, Reuse seed if valid
            if (Seed.TestSeed() == false)
            {
                Seed.SeedIngest();
                Seed.Compressor();
                Seed.CleanSeedInt();
            }
            textBox_seed.Text = data_int.ToString();

            //Create new mod folder with seed name
            string seedFolderName = "StiltzkinsBag2.0-Seed-" + data_int.ToString();
            pathSeed = @tb_fl.Text + seedFolderName;
            DirectoryInfo di = Directory.CreateDirectory(pathSeed);
            DirectoryInfo di2 = Directory.CreateDirectory(pathSeed + "\\StreamingAssets\\Data\\Items");
            DirectoryInfo di3 = Directory.CreateDirectory(pathSeed + "\\StreamingAssets\\Data\\Characters");
            DirectoryInfo di4 = Directory.CreateDirectory(pathSeed + "\\StreamingAssets\\Data\\Characters\\Abilities");

            // Seed done
            // Path done

            // need to read memoria.ini for mods folders to pull CSV/BIN data from
            if (ReadMemoria())
            {
                modNames = CleanModNames(modNames);
                if (CopySourceData(modNames))
                {
                    //Source data copied, ready to do more work
                    // We now have all CSV and BIN Files in our seed folder
                    // So our new Seed folder is prepped with CSV/BIN files to use in randomizatiion.
                }
            }


            // BIN file extraction next

            // We need to write a function to scan and 




            // After writting mods, we should write a Seed_Source.txt and in it, report which mod/stock we are pulling our data from.

            // create txt file with randomized details



            // Finally, we add the seed folder name to the START of the list of MemoriaMods, and then write that line back into the memoria.ini file.
            // once write this method, add to stock button

            // Having our mod first ensures, any *.bytes files and BINs are used in place of other mods.
            // This should in theory, make our mod compatble with all other mods. Alternate Fantasy etc.

            //test
            //modNames.Insert(0, seedFolderName);
            //write memoria file


            //  END MAIN CODE   //
            //  END MAIN CODE   //
            //  END MAIN CODE   //
            //  END MAIN CODE   //


            // Refactor layout of checkbox options.

            
            

        }


        bool ReadMemoria()
        {
            if (File.Exists(@tb_fl.Text + "\\memoria.ini"))
            {
                memoriaINI = File.ReadAllLines(@tb_fl.Text + "\\memoria.ini").ToList();
                for (int i = 0; i < memoriaINI.Count; i++)
                {
                    //need to Identify Mod list
                    if (memoriaINI[i] == "[Mod]")
                    {
                        if (memoriaINI[i + 1].Contains("FolderNames="))
                        {
                            memoriaFolderNamesLine = memoriaINI[i + 1].Substring(12);
                            modNames = memoriaFolderNamesLine.Split(',').ToList();
                            return true;
                        }
                    }
                }
                return true;
            }
            else
            {
                // No Memoria found
                MessageBox.Show("Memoria Engine Required. Moguri and other mods use Memoria engine. If this is a stock FFIX install, you need to at least install Memoria Engine", "Memoria Engine Required", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        List<string> CleanModNames(List<string> modNamesCleaned)
        {
            // modNames.Add(seedFolderName); test only
            for (int i = 0; i < modNamesCleaned.Count; i++)
            {
                if (modNamesCleaned[i].ElementAt(0).ToString() == " ")
                {
                    modNamesCleaned[i] = modNamesCleaned[i].Substring(1);
                }
                if (modNamesCleaned[i].ElementAt(0).ToString() == "\"")
                {
                    modNamesCleaned[i] = modNamesCleaned[i].Substring(1);
                }
                if (modNamesCleaned[i].Substring(modNamesCleaned[i].Length - 1).ToString() == " ")
                {
                    modNamesCleaned[i] = modNamesCleaned[i].Substring(0, modNamesCleaned[i].Length - 1);
                }
                if (modNamesCleaned[i].Substring(modNamesCleaned[i].Length - 1).ToString() == "\"")
                {
                    modNamesCleaned[i] = modNamesCleaned[i].Substring(0, modNamesCleaned[i].Length - 1);
                }
                richTextBox_debug.Text += "\n" + modNamesCleaned[i];
                if (modNamesCleaned[i].Contains("Stiltzkins"))
                {
                    modNamesCleaned.RemoveAt(i);
                }
            }
            return modNamesCleaned;
        }

        bool CopySourceData(List<string> modNamesCopy)
        {
            modNamesCopy.Add(""); //Required, stock StreamingAssests dir
                                  // Search all mod folders for all needed CSV/BIN files and create copy to Seed folder.
            for (int i = 0; i < modNamesCopy.Count; i++)
            {
                List<int> csvIndex = new List<int>();
                for (int j = 0; j < csvNeeded.Count; j++)
                {
                    string csvModPathCheck = @tb_fl.Text + "\\" + modNamesCopy[i] + @csvNeeded[j];
                    string csvSeedPathCheck = pathSeed + csvNeeded[j];
                    if (File.Exists(@csvModPathCheck))
                    {
                        File.Copy(@csvModPathCheck, @csvSeedPathCheck, true);
                        csvIndex.Add(j);
                    }
                }
                if (csvIndex.Count > 0)
                {
                    int counter = 0;
                    for (int j = 0; j < csvIndex.Count; j++)
                    {
                        csvNeeded.RemoveAt(csvIndex[j - counter]);
                        counter++;
                    }
                }
            }
            if (csvNeeded.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void RemoveSBfromMemoria()
        {
            // Remove SB from mod load order

            // read memoria.ini and update mod list
            if (ReadMemoria())
            {
                modNames = CleanModNames(modNames); // removes SB
                
                // need to write modNames back into MemoriaINI then write files back.
                // TODO
            }

        }

        /*if (cm_itemshop.Checked | cm_synth.Checked | cm_char.Checked | cm_enemies.Checked | cm_entrances.Checked)
            {
                if (MessageBox.Show("This will save all Randoms to game files", "Cormfirm Write Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    //reset
                    pbar_tree.Value = pbar_tree.Minimum;

                    if (cm_itemshop.Checked)
                    {
                        Shops.ShopItems();
                        Shops.MedicItems();
                        Shops.ShopCombo();
                    }
                    if (cm_synth.Checked)
                    {
                        Synth.Synthesis();
                    }
                    if (cm_char.Checked)
                    {
                        Char.Character();
                    }
                    richTextBox_debug.Text = richTextBox_debug.Text + "\nrandl: " + randl.ToString() + "\nSeed DNA " + data_int + "\nitems: (1-" + items + ")\n";


                    //writing data needs refactor
                    if (cm_itemshop.Checked | cm_synth.Checked | cm_char.Checked)
                    {
                        ReadWrite();
                    }

                    //Enemies binary needs refactor
                    if (cm_enemies.Checked | cm_entrances.Checked)
                    {
                        Bytes.Bytes_IO();
                    }
                    pbar_tree.Value = pbar_tree.Maximum;
                }
            }*/


        //              MISC FUNCS              //
        //              MISC FUNCS              //



        // Refactor new method
        public void ReadWrite()
        {

            // needs refactor





            button_rand.Text = "Writing Data";
            var t = Task.Run(async delegate
            {
                await Task.Delay(600);
            });
            t.Wait();



            // Refactor Writing data. use Async
            // 

            // use seed folder created before this.  @tb_fl.Text \\ seedfolder \\ CSV locations

            if (cm_itemshop.Checked)
            {
                Directory.SetCurrentDirectory(@tb_fl.Text + "\\StreamingAssets\\Data\\Items");
                File.WriteAllLines("ShopItems.csv", a_comboSafe);
            }
            if (cm_synth.Checked)
            {
                Directory.SetCurrentDirectory(@tb_fl.Text + "\\StreamingAssets\\Data\\Items");
                File.WriteAllLines("Synthesis.csv", a_synthdata);
            }
            if (cm_char.Checked)
            {
                if (c_abilitygems.Checked)
                {
                    Directory.SetCurrentDirectory(@tb_fl.Text + "\\StreamingAssets\\Data\\Characters\\Abilities");
                    File.WriteAllLines("AbilityGems.csv", a_gemdata);
                }
                if (c_basestats.Checked)
                {
                    Directory.SetCurrentDirectory(@tb_fl.Text + "\\StreamingAssets\\Data\\Characters");
                    File.WriteAllLines("BaseStats.csv", a_statdata);
                }
                if (c_default.Checked)
                {
                    Directory.SetCurrentDirectory(@tb_fl.Text + "\\StreamingAssets\\Data\\Characters");
                    File.WriteAllLines("DefaultEquipment.csv", a_equipdata);

                    if (c_all_e.Checked | c_random_e.Checked | c_main_e.Checked)
                    {
                        Directory.SetCurrentDirectory(@tb_fl.Text + "\\StreamingAssets\\Data\\Items");
                        File.WriteAllLines("Items.csv", a_itemdata);
                    }
                }
            }
            button_rand.Text = "Done!";
            t = Task.Run(async delegate
            {
                await Task.Delay(600);
            });
            t.Wait();
            button_rand.Text = "Buy for 333 Gil";
        }


        // Misc UI elements
        private void debugButton_Click(object sender, EventArgs e)
        {
            //Run Debug Form
            DebugForm DebugForm2 = new DebugForm();
            this.Hide();
            DebugForm2.ShowDialog();
            this.Show();
        }

        public string path_search(string pswap)
        {
            string path_search1 = "", path_search2 = "", path_search3 = "";
            RegistryKey rkTest = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\Shell\\MuiCache");
            RegistryKey rkTest2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Compatibility Assistant\\Store");
            RegistryKey rkTest3 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FeatureUsage\\AppSwitched");
            string[] vnames = rkTest.GetValueNames();
            string[] vnames2 = rkTest2.GetValueNames();
            string[] vnames3 = rkTest3.GetValueNames();

            foreach (string s in vnames)
            {
                RegistryValueKind rvk = rkTest.GetValueKind(s);
                if (rvk == RegistryValueKind.String)
                {
                    string value = (string)rkTest.GetValue(s);
                    if (value == "FINAL FANTASY IX")
                    {
                        path_search1 = s.Substring(0, s.IndexOf("FF9_Launcher.exe"));
                    }
                }
            }
            foreach (string s in vnames2)
            {
                if (s.Contains("FF9_Launcher.exe"))
                {
                    path_search2 = s.Substring(0, s.IndexOf("FF9_Launcher.exe"));
                }
            }
            foreach (string s in vnames3)
            {
                if (s.Contains("FF9_Launcher.exe"))
                {
                    path_search3 = s.Substring(0, s.IndexOf("FF9_Launcher.exe"));
                }
            }

            rkTest.Close();
            rkTest2.Close();
            rkTest3.Close();

            // check each
            if (path_search1.Length > 0)
            {
                if (MessageBox.Show(path_search1, "Use this Path?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    pswap = path_search1;
                    pathFound = true;
                }
            }
            if (path_search2.Length > 0)
            {
                if (MessageBox.Show(path_search2, "Use this Path?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    pswap = path_search2;
                    pathFound = true;
                }
            }
            if (path_search3.Length > 0)
            {
                if (MessageBox.Show(path_search3, "Use this Path?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    pswap = path_search3;
                    pathFound = true;
                }
            }
            return pswap;
        }
        public void Manual_search()
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.Description = "locate ffix";
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tb_fl.Text = folderDlg.SelectedPath;
                pathFound = true;
            }
        }
        private void b_restore_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will remove Stilzkin's Bag from the Mod Load Order, Seed folders will remain", "Remove Stiltzkin's Bag", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                RemoveSBfromMemoria();
            }
        }
        private void richTextBox_debug_TextChanged(object sender, EventArgs e)
        {
            richTextBox_debug.SelectionStart = richTextBox_debug.Text.Length;
            richTextBox_debug.ScrollToCaret();
        }
        private void b_rseed_Click_1(object sender, EventArgs e)
        {
            Random rnd = new Random();
            textBox_seed.Text = rnd.Next(100000000, 999999999).ToString();
        }
        private void b_open_Click(object sender, EventArgs e)
        {
            Manual_search();
        }
        private void b_search_Click(object sender, EventArgs e)
        {
            pswap = "";
            pswap = path_search(pswap);
            if (pswap.Length > 0)
            {
                tb_fl.Text = pswap;
            }
        }
        private void cm_itemshop_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        private void cm_synth_CheckedChanged(object sender, EventArgs e)
        {
            if (cm_synth.Checked)
            {
                c_require.Enabled = true;
                c_result.Enabled = true;
                c_prices.Enabled = true;
            }
            else if (!cm_synth.Checked)
            {
                c_require.Enabled = false;
                c_result.Enabled = false;
                c_prices.Enabled = false;
            }
        }
        private void cm_char_CheckedChanged(object sender, EventArgs e)
        {
            if (cm_char.Checked)
            {
                c_default.Enabled = true;
                c_basestats.Enabled = true;
                c_abilitygems.Enabled = true;
                if (!c_default.Checked & !c_basestats.Checked & !c_abilitygems.Checked)
                {
                    c_default.Checked = true; c_basestats.Checked = true;
                }
            }
            else if (!cm_char.Checked)
            {
                c_default.Enabled = false;
                c_basestats.Enabled = false;
                c_abilitygems.Enabled = false;
            }
        }
        private void c_basestats_CheckedChanged(object sender, EventArgs e)
        {
            if (c_basestats.Checked)
            {
                c_basestatsBool = true;
            }
            if (!c_basestats.Checked)
            {
                c_basestatsBool = false;
            }
        }
        private void c_abilitygems_CheckedChanged(object sender, EventArgs e)
        {
            if (c_abilitygems.Checked)
            {
                c_abilitygemsBool = true;
            }
            if (!c_abilitygems.Checked)
            {
                c_abilitygemsBool = false;
            }
        }
        private void c_default_CheckedChanged(object sender, EventArgs e)
        {
            if (c_default.Checked)
            {
                c_defaultBool = true;
                c_all_e.Enabled = true;
                c_random_e.Enabled = true;
                c_main_e.Enabled = true;

                if (!c_all_e.Checked & !c_random_e.Checked & !c_main_e.Checked)
                {
                    c_random_e.Checked = true;
                }
            }
            if (!c_default.Checked)
            {
                c_defaultBool = false;
                c_all_e.Enabled = false;
                c_random_e.Enabled = false;
                c_main_e.Enabled = false;
            }
        }
        private void c_default_EnabledChanged(object sender, EventArgs e)
        {
            if (!c_default.Enabled)
            {
                c_all_e.Enabled = false;
                c_random_e.Enabled = false;
                c_main_e.Enabled = false;
            }
            else if (c_default.Enabled & c_default.Checked)
            {
                c_all_e.Enabled = true;
                c_random_e.Enabled = true;
                c_main_e.Enabled = true;
            }
        }
        private void c_main_e_CheckedChanged(object sender, EventArgs e)
        {
            if (c_main_e.Checked)
            {
                c_main_eBool = true;
                c_random_e.Checked = false;
                c_all_e.Checked = false;
            }
            if (!c_main_e.Checked)
            {
                c_main_eBool = false;
            }
        }
        private void c_random_e_CheckedChanged(object sender, EventArgs e)
        {
            if (c_random_e.Checked)
            {
                c_random_eBool = true;
                c_random_eEn = true;
                c_all_e.Checked = false;
                c_main_e.Checked = false;
            }
            if (!c_random_e.Checked)
            {
                c_random_eBool = false;
                c_random_eEn = false;
            }
        }
        private void c_all_e_CheckedChanged(object sender, EventArgs e)
        {
            if (c_all_e.Checked)
            {
                c_all_eEn = true;
                c_all_eBool = true;
                c_random_e.Checked = false;
                c_main_e.Checked = false;
            }
            if (!c_all_e.Checked)
            {
                c_all_eEn = false;
                c_all_eBool = false;
            }

        }
        private void pbar_tree_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void richTextBox_output_TextChanged(object sender, EventArgs e)
        {
            richTextBox_output.SelectionStart = richTextBox_output.Text.Length;
            richTextBox_output.ScrollToCaret();
        }
        private void c_require_CheckedChanged(object sender, EventArgs e)
        {
            if (c_require.Checked)
            {
                c_requireBool = true;
            }
            else if (!c_require.Checked)
            {
                c_requireBool = false;
            }
        }
        private void c_result_CheckedChanged(object sender, EventArgs e)
        {
            if (c_result.Checked)
            {
                c_resultBool = true;
            }
            else if (!c_result.Checked)
            {
                c_resultBool = false;
            }
        }
        private void c_prices_CheckedChanged(object sender, EventArgs e)
        {
            if (c_prices.Checked)
            {
                c_pricesBool = true;
            }
            else if (!c_prices.Checked)
            {
                c_pricesBool = false;
            }
        }
        //test
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //          change source selection
            // part of Field Randomizing, ignoring for now
            var instance = new Logic();
            instance.LogicOut();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //          background pic
        }
        private void tb_fl_TextChanged(object sender, EventArgs e)
        {
            tb_flText = @tb_fl.Text;
        }
        private void textBox_seed_TextChanged(object sender, EventArgs e)
        {
            textBoxSeed = textBox_seed.Text;
        }
        private void cm_treasure_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void cm_stiltzkin_CheckedChanged(object sender, EventArgs e)
        {

        }
        //Entrances
        private void cm_entrances_CheckedChanged(object sender, EventArgs e)
        {
            //          random entrances selection
            if (cm_entrances.Checked)
            {
                /*c_allpaths.Enabled = true;
                c_nologic.Enabled = true;
                if (!c_nologic.Checked & !c_allpaths.Checked)
                {
                    c_allpaths.Checked = true;
                }*/
            }
            else if (!cm_entrances.Checked)
            {
                /*c_allpaths.Enabled = false;
                c_nologic.Enabled = false;*/
            }
        }
        private void c_nologic_CheckedChanged(object sender, EventArgs e)
        {
            // connect randomly with no gaurenteed completion path
            /*if (c_nologic.Checked)
            {
                c_allpaths.Checked = false;
            }*/
        }
        private void c_allpaths_CheckedChanged(object sender, EventArgs e)
        {
            // connect randomly and make sure all paths connect to all exits
            /*if (c_allpaths.Checked)
            {
                c_nologic.Checked = false;
            }*/
        }
        //Enemies
        private void cm_enemies_CheckedChanged(object sender, EventArgs e)
        {
            //          random enemies selection
            if (EnemyCheckBox.Checked)
            {
                EnemyStealsCheckBox.Enabled = true;
                EnemyDropsCheckBox.Enabled = true;
                if (!EnemyStealsCheckBox.Checked & !EnemyDropsCheckBox.Checked)
                {
                    EnemyStealsCheckBox.Checked = true;
                    EnemyDropsCheckBox.Checked = true;
                }
                if (EnemyStealsCheckBox.Checked)
                {
                    EnemyBluMagCheckBox.Enabled = true;
                    //c_es2.Enabled = true;
                    //c_es3.Enabled = true;
                    c_es4.Enabled = true;
                }
                if (EnemyDropsCheckBox.Checked)
                {
                    EnemyCardCheckBox.Enabled = true;
                    //c_ed2.Enabled = true;
                    //c_ed3.Enabled = true;
                    c_ed4.Enabled = true;
                }
            }
            else if (!EnemyCheckBox.Checked)
            {
                EnemyStealsCheckBox.Enabled = false;
                EnemyDropsCheckBox.Enabled = false;
                EnemyBluMagCheckBox.Enabled = false;
                //c_es2.Enabled = false;
                //c_es3.Enabled = false;
                c_es4.Enabled = false;
                EnemyCardCheckBox.Enabled = false;
                //c_ed2.Enabled = false;
                //c_ed3.Enabled = false;
                c_ed4.Enabled = false;
            }

        }
        private void c_esteals_CheckedChanged(object sender, EventArgs e)
        {
            //      random steals selection
            if (EnemyStealsCheckBox.Checked)
            {
                EnemyBluMagCheckBox.Enabled = true;
                //c_es2.Enabled = true;
                //c_es3.Enabled = true;
                c_es4.Enabled = true;
                if (!EnemyBluMagCheckBox.Checked & /*!c_es2.Checked & !c_es3.Checked &*/ !c_es4.Checked)
                {
                    //c_es3.Checked = true;
                    c_es4.Checked = true;
                }
            }
            else if (!EnemyStealsCheckBox.Checked)
            {
                EnemyBluMagCheckBox.Enabled = false;
                //c_es2.Enabled = false;
                //c_es3.Enabled = false;
                c_es4.Enabled = false;
            }
        }
        private void c_edrops_CheckedChanged(object sender, EventArgs e)
        {
            //      random drops selection
            if (EnemyDropsCheckBox.Checked)
            {
                EnemyCardCheckBox.Enabled = true;
                //c_ed2.Enabled = true;
                //c_ed3.Enabled = true;
                c_ed4.Enabled = true;
                if (!EnemyCardCheckBox.Checked & /*!c_ed2.Checked & !c_ed3.Checked &*/ !c_ed4.Checked)
                {
                    //c_ed3.Checked = true;
                    c_ed4.Checked = true;
                }
            }
            else if (!EnemyDropsCheckBox.Checked)
            {
                EnemyCardCheckBox.Enabled = false;
                //c_ed2.Enabled = false;
                //c_ed3.Enabled = false;
                c_ed4.Enabled = false;
            }
        }
        private void c_es1_CheckedChanged(object sender, EventArgs e)
        {
            // steal slot 1
        }
        private void c_es4_CheckedChanged(object sender, EventArgs e)
        {
            // steal slot 4
        }
        private void c_ed1_CheckedChanged(object sender, EventArgs e)
        {
            // drop slot 1
        }
        private void c_ed4_CheckedChanged(object sender, EventArgs e)
        {
            // drop slot 4
        }
        private void Serial_But_CheckedChanged(object sender, EventArgs e)
        {
            /*if (Serial_But.Checked)
            {
                bool c_serial = true;
            }*/
        }


    }


}