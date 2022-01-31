using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        string num = "70+82*79-78+84/73+83";

        DataTable dt = new DataTable();
        dt.Columns.Add("string");

        string tempStr = "";
        //string oper = "";
        foreach (char str in num)
        {
            if (str == '+' || str == '-' || str == '*' || str == '/')
            {
                // add number to datable
                dt.Rows.Add(tempStr);
                // clear after that
                tempStr = "";

                // store operator
                tempStr = str.ToString();
                dt.Rows.Add(tempStr);
                tempStr = "";
                // add to datable and clear
            }
            else
            {
                // store number only not add
                tempStr += str.ToString();
            }
        }

        // add last number
        dt.Rows.Add(tempStr);
        tempStr = "";

        decimal total = 0;

        int num1 = 0;
        int num2 = 0;

        DataTable dtTemp = new DataTable();
        dtTemp.Columns.Add("string");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][0].ToString() == "*")
            {
                total = 0;
                num1 = Convert.ToInt32(dt.Rows[i - 1][0].ToString());
                num2 = Convert.ToInt32(dt.Rows[i + 1][0].ToString());

                total = num1 * num2;
                dtTemp.Rows.RemoveAt(dtTemp.Rows.Count - 1);
                dtTemp.Rows.Add(total.ToString());
                i += 1;
            }
            else if (dt.Rows[i][0].ToString() == "/")
            {
                total = 0;
                num1 = Convert.ToInt32(dt.Rows[i - 1][0].ToString());
                num2 = Convert.ToInt32(dt.Rows[i + 1][0].ToString());

                total = (decimal)num1 / (decimal)num2;
                dtTemp.Rows.RemoveAt(dtTemp.Rows.Count - 1);
                dtTemp.Rows.Add(total.ToString());
                i += 1;
            }
            else if (dt.Rows[i][0].ToString() == "+")
            {
                if (dtTemp.Rows.Count == 0)
                {
                }
                else
                {
                    if (dtTemp.Rows[dtTemp.Rows.Count - 1][0].ToString() == "+")
                    {
                        // nothing
                    }
                    else if (dtTemp.Rows[dtTemp.Rows.Count - 1][0].ToString() == "-")
                    {
                        // nothing
                    }
                    else
                    {
                        dtTemp.Rows.Add(dt.Rows[i][0].ToString());
                    }
                }
            }
            else if (dt.Rows[i][0].ToString() == "-")
            {
                if (dtTemp.Rows.Count == 0)
                {
                }
                else
                {
                    if (dtTemp.Rows[dtTemp.Rows.Count - 1][0].ToString() == "+")
                    {
                        dtTemp.Rows.RemoveAt(i - 1);
                        dtTemp.Rows.Add(dt.Rows[i][0].ToString());
                    }
                    else if (dtTemp.Rows[dtTemp.Rows.Count - 1][0].ToString() == "-")
                    {
                        // nothing
                    }
                    else
                    {
                        dtTemp.Rows.Add(dt.Rows[i][0].ToString());
                    }
                }
            }
            else
            {
                dtTemp.Rows.Add(dt.Rows[i][0].ToString());
            }
        }

        decimal sumTotal = 0;
        decimal tempNumber = 0;
        string oper = "";
        for (int i = 0; i < dtTemp.Rows.Count - 1; i++)
        {
            if (dtTemp.Rows[i][0].ToString() == "+")
            {
                oper = "+";
                continue;
            }
            else if (dtTemp.Rows[i][0].ToString() == "-")
            {
                oper = "-";
                continue;
            }
            else
            {
                tempNumber = Convert.ToDecimal(dtTemp.Rows[i][0].ToString());
                if (oper == "+")
                {
                    sumTotal += tempNumber;
                }
                else if (oper == "-")
                {
                    sumTotal -= tempNumber;
                }
                else
                {
                    sumTotal = tempNumber;
                }
            }
        }
        Console.WriteLine($"result => {sumTotal.ToString()}");
        Console.ReadKey();
    }

    private static void Test(int index)
    {
    }
}