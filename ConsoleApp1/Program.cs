using System;
using System.Collections.Generic;
using System.Linq;
using SCB.Client;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SCBClient client = new SCBClient("api.scb.se/OV0104");

            SCBNode current = client.TopNode;

            while (true)
            {
                List<SCBNode> nodes = new List<SCBNode>();
                if (current.IsLevel)
                {
                    nodes = client.GetNodesBelow(current);
                    Console.WriteLine($"{0}. Go up");
                    PresentNodes(nodes);
                }
                else if(current.IsTable)
                {
                    SCBMetaData metaData = client.GetMetaData(current.path);
                    PresentMetaData(metaData);

                    bool newQuery = true;

                    while (newQuery)
                    {
                        QueryTable(metaData, client, current);
                        Console.Out.Write("New query? [Y/N] ");
                        string ans = Console.In.ReadLine();
                        newQuery = ans.ToLower().Equals("y");
                    }
                    Console.WriteLine($"{0}. Go up");
                }

                bool isNumber = false;
                int index = 0;
                while (!isNumber)
                {
                    Console.Out.Write("Select an item: ");
                    string selection = Console.In.ReadLine();
                    isNumber = int.TryParse(selection, out index);
                }

                if (index == 0)
                {
                    if (!current.path.Equals(client.TopNode.path))
                    {
                        current.path = current.ParentPath();
                        current.type = "l";
                    }
                }
                else
                {
                    SCBNode selectedNode = nodes[index - 1];
                    if (selectedNode != null)
                    {
                        current = selectedNode;
                    }
                }
            }

        }

        private static void QueryTable(SCBMetaData metaData, SCBClient client, SCBNode current)
        {
            SCBQuery query = new SCBQuery();
            foreach (SCBVariable variable in metaData.variables)
            {
                SCBQueryItem qi = new SCBQueryItem
                {
                    code = variable.code,
                    selection = new SCBSelection {filter = "top"}
                };

                Console.Out.Write($"{variable.code} : ");
                Console.Out.Write("Filter [item,all,top,agg,quit] = ");
                bool ok = false;
                while (!ok)
                {
                    string ans = Console.In.ReadLine();

                    string shortString = (ans.Length < 2) ? String.Empty : (ans ?? string.Empty).Substring(0, 2).ToLower();

                    switch (shortString)
                    {
                        case "it":
                            qi.selection.values.AddRange(GetItems(variable));
                            qi.selection.filter = "item";
                            ok = true;
                            break;
                        case "al":
                            qi.selection.values.Add("*");
                            qi.selection.filter = "all";
                            ok = true;
                            break;
                        case "to":
                            qi.selection.values.Add(GetTop().ToString());
                            qi.selection.filter = "top";
                            ok = true;
                            break;
                        case "ag":
                            qi.selection.filter = "agg";
                            ok = true;
                            break;
                        case "qu":
                            ok = true;
                            break;
                        default:
                            break;
                    }

                    if (!ok)
                    {
                        Console.Out.WriteLine("Invalid input, please try again, or Quit!");
                        Console.Out.Write("Filter [Item,All,Top,Agg,Quit] = ");
                    }

                    ;
                }


                query.query.Add(qi);
            }


            SCBTable table = client.GetTable(current, query);

            PresentTable(table);
        }

        static List<string> GetItems(SCBVariable variable)
        {
            List<string> items = new List<string>();

            bool ok = false;
            int itemCnt = 0;
            while (!ok)
            {
                Console.Out.Write($"Item {++itemCnt} = ");
                string ans = Console.In.ReadLine();

                
                if (ans.ToLower().Equals("q"))
                {
                    break;
                }

                if (variable.values.Contains(ans))
                {
                    items.Add(ans);
                }
                else
                {
                    Console.Out.WriteLine("Not a valid value. Please try again or Quit!");
                }
            }
            return items;
        }

        static int GetTop()
        {
            int n = 0;
            bool ok = false;
            while (!ok)
            {
                Console.Out.Write("N to list = ");
                string ntop = Console.In.ReadLine();
                ok = int.TryParse(ntop, out n);
                if (!ok)
                {
                    Console.Out.WriteLine("Not valid. Please input an integer >= 1.");
                }
            }

            return n;
        }

        static void PresentNodes(List<SCBNode> nodes)
        {
            if(nodes== null) return;
            foreach (SCBNode node in nodes)
            {
                Console.WriteLine($"{nodes.IndexOf(node)+1:f0}. {node.id} {node.type} {node.text}");
            }
        }

        static void PresentMetaData(SCBMetaData md)
        {
            Console.WriteLine(md.title);
            foreach (SCBVariable scbVariable in md.variables)
            {
                Console.WriteLine($"======================={scbVariable.code}======================================");
                Console.WriteLine($"{scbVariable.text}");

                if (scbVariable.time)
                {
                    for(int i = 0; i < scbVariable.values.Count; i++)
                    {
                        Console.Write($"{scbVariable.values[i]} ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    for (int i = 0; i < scbVariable.values.Count; i++)
                    {
                        Console.WriteLine($"{scbVariable.values[i]} {scbVariable.valueTexts[i]}");
                    }
                }
                Console.WriteLine($"Elimination = {scbVariable.elimination}");
                Console.WriteLine($"Time = {scbVariable.time}");
                Console.WriteLine("================================================================================");
            }
            
        }

        

        static void PresentTable(SCBTable table)
        {
            foreach (SCBColumn tableColumn in table.columns)
            {
                if (tableColumn.type == "c" && !string.IsNullOrEmpty(tableColumn.unit))
                {
                    Console.Write($"{tableColumn.text} [{tableColumn.unit}] ");
                }
                else
                {
                    Console.Write($"{tableColumn.text} ");
                }
            }
            Console.WriteLine();
            foreach (SCBTableEntry scbTableEntry in table.data)
            {
                foreach (string s in scbTableEntry.key)
                {
                    Console.Write($"{s} ");
                }

                foreach (string s in scbTableEntry.values)
                {
                    Console.Write($"{s} ");
                }
                Console.WriteLine();
            }
        }

    }
}
