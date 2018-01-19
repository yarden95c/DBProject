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
            // Console.WriteLine(cont.GetPlace("a", "B"));
            // List<string> result=cont.GetTopPlacesNames("flor");

              SignInController sign = SignInController.GetInstance();
              Console.WriteLine(sign.SignIn("sed.turpis@suscipit.org", "BFI65VCD0AG"));
              User user = sign.ConnectedUser;


            /*   NumberExecuter number = new NumberExecuter(user, DataBaseConnector.GetInstance());
               for (int i = 0; i < 10; i++)
               {
                   Console.WriteLine(number.Execute());
               } */
            /*   user.GenreId = 13;
               PlaceExecuter place = new PlaceExecuter("florida", DataBaseConnector.GetInstance(), user);
               for (int i = 0; i < 10; i++)
               {
                   Console.WriteLine(place.Execute());
               } */

            /*  YearExecuter year = new YearExecuter(DataBaseConnector.GetInstance(), 2000, 2011,user);
              for (int i = 0; i < 10; i++)
              {
                  Console.WriteLine(year.Execute());
              } */

            /*  HitMeWithController cont = new HitMeWithController();
              List<string> list = cont.GetTopGenresNames("");
              GenreExecuter genre = new GenreExecuter("", DataBaseConnector.GetInstance(), user);
              for (int i = 0; i < 10; i++)
              {
                  Console.WriteLine(genre.Execute());
              } */

            FeelingLuckyController luck = new FeelingLuckyController();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(luck.GetResult());
            }

            while (true) ;
        }
    }
}
