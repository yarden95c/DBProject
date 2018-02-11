using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseLayer;
using DataBaseLayer.Entities;
using MySql.Data.MySqlClient;
using Controllers;


namespace Test
{
    class Program
    {
        delegate void del(string str);

        static void Main(string[] args)
        {
            /*  del d = str => Console.WriteLine("1: {0}",str);
              d+= str => Console.WriteLine("2: {0}", str);
              d += str => Console.WriteLine("3: {0}", str);
              d += str => Console.WriteLine("4: {0}", str);
              d += str => Console.WriteLine("5: {0}", str);
              d += str => Console.WriteLine("6: {0}", str);

              d("msg");
              Console.WriteLine("del length: {0}", d.GetInvocationList().Length); */


            //DataBaseConnector conn = new DataBaseConnector();
            /*MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from artists limit 10";
            List<Dictionary<string, string>> res = conn.ExecuteCommand(comm); */
            // IKnowWhatIWantController cont = new IKnowWhatIWantController();
            // Console.WriteLine(cont.GetSong("","the spinners",0,9999));
            // Console.WriteLine(cont.GetArtist("the SPInners", "", 0, 9999));
            // Console.WriteLine(cont.GetPlace("a", "B"));
            // List<string> result=cont.GetTopPlacesNames("flor");

            /*   SignInController sign = SignInController.GetInstance();
               Console.WriteLine(sign.SignIn("sed.turpis@suscipit.org", "BFI65VCD0AG"));
               User user = sign.ConnectedUser; */


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

            /*   FeelingLuckyController luck = new FeelingLuckyController();
               for (int i = 0; i < 10; i++)
               {
                   Console.WriteLine(luck.GetResult());
               } */

            /*   SignUpController up = new SignUpController();
               Console.WriteLine(up.SignUp("first name3", "last name3", "email3@email.com", 1, 2, 2000, "password", "ROCK", "isRael"));
               List<string> songs = new List<string> { "I want to hold your hand", "sound of silence","bla","don't cry for me argentina" };
               List<string> artists = new List<string> {"art garfunkel","coldplay","radiohead","mozart","foo fighters" };
               Console.WriteLine(up.AddSongs(songs));
               Console.WriteLine(up.AddArtists(artists));
               Console.WriteLine(SignInController.GetInstance().ConnectedUser.Email);
               foreach (string songId in SignInController.GetInstance().ConnectedUser.Songs)
               {
                   Song song = EntitiesFactory.GetSongFromSongId(songId, DataBaseLayer.DataBaseConnector.GetInstance());
                   Console.WriteLine("song name: {0}, song id: {1}", song.Name, song.Id);
               }

               foreach (int artistId in SignInController.GetInstance().ConnectedUser.Artists)
               {
                   Artist artist = EntitiesFactory.GetArtistFromArtistId(artistId.ToString(), DataBaseLayer.DataBaseConnector.GetInstance());
                   Console.WriteLine("artist name: {0}, artist id: {1}", artist.Name, artist.Id);
               } */

            SignInController signIn = SignInController.GetInstance();
            signIn.SignIn("scelerisque.dui@pedenonummy.org", "JRF51YHU3GE");

            FeelingLuckyController lucky = new FeelingLuckyController();
            Console.WriteLine(lucky.GetResult());

            HitMeWithController hitMe = new HitMeWithController();
            Console.WriteLine(hitMe.GetPlace(""));
            Console.WriteLine(hitMe.GetPlace("autralia"));
            Console.WriteLine(hitMe.GetGenre("hip hop"));
            Console.WriteLine(hitMe.GetNumber());
            Console.WriteLine(hitMe.GetYear(2000,2018));
            Console.WriteLine(hitMe.GetYear(-1, -1));



            while (true) ;
        }
    }
}
