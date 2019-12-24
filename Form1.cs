using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Dempster_Shafer
{

    public partial class Form1 : Form
    {
        string[]  lines;
        List<string> hypos = new List<string>();
        Dictionary<string, List<double>> hyposB = new Dictionary<string, List<double>>();


        List<string> evids = new List<string>();
        List<HashSet<string>> evidHypo = new List<HashSet<string>>();
        List<HashSet<string>> counterevidHypo = new List<HashSet<string>>();

        List<double> evidsWeight = new List<double>();
        List<int> checkedEvids = new List<int>();


        List<string> addEvids = new List<string>();
        List<HashSet<string>> addevidHypo = new List<HashSet<string>>();
        List<HashSet<string>> counteraddevidHypo = new List<HashSet<string>>();
        List<double> addevidsWeight = new List<double>();
        Dictionary<string, List<double>> addB = new Dictionary<string, List<double>>();


        void init()
        {
            hypos = new List<string>();
            hyposB = new Dictionary<string, List<double>>();

            evids = new List<string>();
            evidHypo = new List<HashSet<string>>();
            counterevidHypo = new List<HashSet<string>>();

           evidsWeight = new List<double>();
           checkedEvids = new List<int>();


            addEvids = new List<string>();
            addevidHypo = new List<HashSet<string>>();
            counteraddevidHypo = new List<HashSet<string>>();
            addevidsWeight = new List<double>();
            addB = new Dictionary<string, List<double>>();
            lines = File.ReadAllLines("base.data");
            int j = 0; ;
            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i] == "|")
                {
                    j = i + 1;
                    break;
                }
                hypos.Add(lines[i]);
            }
            for (int i = j; i < lines.Length; ++i)
            {
                if (lines[i] == "|")
                {
                    j = i + 1;
                    break;
                }
                var parse = lines[i].Split('|');
                evids.Add(parse[0]);
                evidHypo.Add(new HashSet<string>());
                foreach (var x in parse[1].Split(','))
                    evidHypo[evidHypo.Count() - 1].Add(x);
                evidsWeight.Add(Convert.ToDouble(parse[2], System.Globalization.CultureInfo.InvariantCulture));
                counterevidHypo.Add(new HashSet<string>());
                foreach (var x in parse[3].Split(','))
                    counterevidHypo[counterevidHypo.Count() - 1].Add(x);
                if (!EvidList.Items.Contains(parse[0]))
                    EvidList.Items.Add(parse[0]);
            }
            for (int i = j; i < lines.Length; ++i)
            {
                var parse = lines[i].Split('|');
                addEvids.Add(parse[0]);
                addevidHypo.Add(new HashSet<string>());
                foreach (var x in parse[1].Split(','))
                    addevidHypo[addevidHypo.Count() - 1].Add(x);
                addevidsWeight.Add(Convert.ToDouble(parse[2], System.Globalization.CultureInfo.InvariantCulture));
                counteraddevidHypo.Add(new HashSet<string>());
                foreach (var x in parse[3].Split(','))
                    counteraddevidHypo[counteraddevidHypo.Count() - 1].Add(x);
            }
        }

        public Form1()
        {
            InitializeComponent();
            init();

        }

        private void mainButton_Click(object sender, EventArgs e)
        {
            init();

            foreach (var x in EvidList.CheckedItems)
                checkedEvids.Add(evids.IndexOf(x.ToString()));

            
            double sumWeights = 0;
            foreach (var i in checkedEvids)
                sumWeights += evidsWeight[i];
            double divider = 1 / sumWeights;
            foreach (var i in checkedEvids)
            {
                HashSet<string> toAdd = new HashSet<string>();

                foreach (var x in evidHypo[i])
                {
                    if (addEvids.Contains(x))
                    {
                        if (!addB.ContainsKey(x))
                        {
                            addB[x] = new List<double>();
                            addB[x].Add(0);
                            addB[x].Add(1);
                            addB[x].Add(0);
                        }
                        addB[x][0]+=evidsWeight[i] * divider;
                       // foreach (var y in addevidHypo[addEvids.IndexOf(x)])
                        //{
                     //       if (!evidHypo[i].Contains(y))
                    //           toAdd.Add(y);
                    //    }
                        foreach (var y in counteraddevidHypo[addEvids.IndexOf(x)])
                        {
                            if (!counterevidHypo[i].Contains(y))
                                toAdd.Add(y);
                        }
                    }
                }
                foreach (var x in counterevidHypo[i])
                {
                    if (x == "") break;
                    if (evids.Contains(x))
                    {
                        foreach (var y in evidHypo[evids.IndexOf(x)])
                            if (addEvids.Contains(y))
                            {
                                if (!addB.ContainsKey(y))
                                {
                                    addB[y] = new List<double>();
                                    addB[y].Add(0);
                                    addB[y].Add(1);
                                    addB[y].Add(0);
                                }
                                addB[y][1] -= evidsWeight[i] * divider;

                            }
                    }
                    else
                    {
                        if (addEvids.Contains(x))
                        {
                            if (!addB.ContainsKey(x))
                            {
                                addB[x] = new List<double>();
                                addB[x].Add(0);
                                addB[x].Add(1);
                                addB[x].Add(0);
                            }
                            addB[x][1] -= evidsWeight[i] * divider;
                        }
                    }
                }
                /*
                foreach (var k in toAdd)
                    counterevidHypo[i].Add(k);
                  */  
            }

            int cnt = 0;
            foreach (var x in addB)
            {
                ++cnt;
                int i = addEvids.IndexOf(x.Key);
                evids.Add(x.Key);
                evidHypo.Add(addevidHypo[i]);
                counterevidHypo.Add(counteraddevidHypo[i]);
                evidsWeight.Add(addevidsWeight[i]* x.Value[0]*x.Value[1]);

                checkedEvids.Add(evids.Count() - 1);
            }

            addB = new Dictionary<string, List<double>>();


            sumWeights = 0;
            foreach (var i in checkedEvids)
            sumWeights += evidsWeight[i];
            divider = 1 / sumWeights;
            foreach (var i in checkedEvids)
            {
                foreach (var x in evidHypo[i])
                {
                    if (hypos.Contains(x))
                    {
                        if (!hyposB.ContainsKey(x))
                        {
                            hyposB[x] = new List<double>();
                            hyposB[x].Add(0);
                            hyposB[x].Add(1);
                            hyposB[x].Add(0);
                        }
                        hyposB[x][0]+=evidsWeight[i] * divider;
                  

                    }
                }
                foreach (var x in counterevidHypo[i])
                {
                    foreach (var n in evids)
                    {
                        if (n == x)
                        {
                            int t = evids.IndexOf(n);
                            foreach (var d in evidHypo[t])
                            {
                                if (hypos.Contains(d))
                                {
                                    if (!hyposB.ContainsKey(d))
                                    {
                                        hyposB[d] = new List<double>();
                                        hyposB[d].Add(0);
                                        hyposB[d].Add(1);
                                        hyposB[d].Add(0);
                                    }
                                    hyposB[d][1] -= evidsWeight[i] * divider;
                                }

                            }
                        }
                    }
                }
            }
            label2.Text = "";
            double max = 0;
            string max_name = "";
            foreach (var x in hyposB)
            {
                if ((x.Value[1] * x.Value[0]) > max)
                {
                    max = x.Value[1] * x.Value[0];
                    max_name = x.Key;
                }
                label2.Text += x.Key + ' ' + Math.Round(x.Value[0],3).ToString() + ' ' + Math.Round(x.Value[1], 3).ToString() + ' ' + Math.Round(x.Value[1]*x.Value[0], 3).ToString() + '\n';
            }
            label2.Text += "\nНаиболее вероятный:\n" + max_name+' ' + max.ToString() + '\n';



        }
    }
}
