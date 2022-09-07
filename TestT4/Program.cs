
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

string a = "d d ";
a.Replace(" ", "");
void CreateModel(string path, string text)
{
    FileInfo dataFile = new FileInfo(path);
    BinaryWriter bw = new BinaryWriter(dataFile.OpenWrite());
    bw.Write(text);
    bw.Close();
}

//var result = Directory.GetCurrentDirectory();
//if (!Directory.Exists(result + "\\Models"))
//{
//    Directory.CreateDirectory(result + "\\Models");
//}



const string connection = "server=CANR2-10\\SQLEXPRESS;database=Northwind ;Integrated Security=True;MultipleActiveResultSets=True;";
SqlConnection con = new SqlConnection(connection);
SqlCommand cmd = new SqlCommand("SELECT * FROM sys.tables", con);
if (con.State == System.Data.ConnectionState.Closed)
    con.Open();

SqlDataReader dr = cmd.ExecuteReader();
while (dr.Read())
{

    Console.WriteLine("-----------------------------" + dr[0] + "------------------------------------");
    SqlCommand propCommand = new SqlCommand($"SELECT COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{dr[0]}' ORDER BY ORDINAL_POSITION", con);
    SqlDataReader dr1 = propCommand.ExecuteReader();
    while (dr1.Read())
    {
        SqlDbType type = (SqlDbType)Enum.Parse(typeof(SqlDbType), dr1[1].ToString(), true);
        Console.WriteLine(dr1[0] + "  --  " + GetType(type));
    }
}

dr.Close();
con.Close();




string GetType(SqlDbType type)
{
    switch (type)
    {
        case SqlDbType.Int:
            return "int";
            break;
        case SqlDbType.Float:
            return "float";
            break;
        case SqlDbType.Char:
        case SqlDbType.NChar:
        case SqlDbType.NText:
        case SqlDbType.NVarChar:
        case SqlDbType.Text:
        case SqlDbType.VarChar:
        case SqlDbType.Xml:
            return "string";
            break;
        case SqlDbType.Decimal:
        case SqlDbType.Money:
        case SqlDbType.SmallMoney:
            return "decimal";
            break;
        case SqlDbType.SmallInt:
            return "short";
            break;
        case SqlDbType.DateTime:
        case SqlDbType.SmallDateTime:
        case SqlDbType.Date:
        case SqlDbType.Time:
        case SqlDbType.DateTime2:
            return "DateTime";
            break;
        case SqlDbType.BigInt:
            return "long";

        case SqlDbType.Binary:
        case SqlDbType.Image:
        case SqlDbType.Timestamp:
        case SqlDbType.VarBinary:
            return "byte[]";

        case SqlDbType.Bit:
            return "bool";

        default:
            return "object";
            break;
    }
}