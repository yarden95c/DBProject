using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;
using MySql.Data.MySqlClient;
using Controllers;
namespace Test
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //DataBaseConnector conn = new DataBaseConnector();
            /*MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from artists limit 10";
            List<Dictionary<string, string>> res = conn.ExecuteCommand(comm); */
           // IKnowWhatIWantController cont = new IKnowWhatIWantController();
          // Console.WriteLine(cont.GetSong("","the spinners",0,9999));
           // Console.WriteLine(cont.GetArtist("the SPInners", "", 0, 9999));
            // Console.WriteLine(cont.GetPlace("f", ""));
            // List<string> result=cont.GetTopPlacesNames("flor");

            SignInController sign = SignInController.GetInstance();
            Console.WriteLine(sign.SignIn("dui@Craseu.net", "AG48LEC1GB"));
            User user = sign.ConnectedUser;
            while (true) ;
        }
    }
}
