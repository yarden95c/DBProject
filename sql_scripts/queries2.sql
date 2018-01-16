select area_name, artist_name from area left join artists using (id_area) limit 10;

select area_name, artist_name from (select area_name, IFNULL(artist_name, "") as artist_name from area left join artists using (id_area)) as t where lower(area_name) like "%flo%" order by area_name;

select area_name, artist_name from area left join artists using (id_area) where lower(artist_name) like "%garf%" order by area_name ;

select area_name, artist_name from area left join artists using (id_area) where lower(area_name) like "%a%" and lower(artist_name) like "%b%"  limit 10;

select song_name,release_date_year,artist_name 
                                  from songs left join songsbyartist using(id_song) left join artists using(id_artist)
                                  where lower(song_name) like "%sound%" 
                                  and release_date_year between 0 and 9999 
								and lower(artist_name) like "%garf%" limit 10;
                                
create view songs_view as select song_name,release_date_year,artist_name from songs left join songsbyartist using(id_song) left join artists using(id_artist) ;

select * from songs_view where lower(song_name) like "%sound%"  limit 10;

select song_name,release_date_year,artist_name from songs left join songsbyartist using(id_song) left join artists using(id_artist) where lower(song_name) like "%sound%" limit 10;

select song_name,release_date_year,artist_name from songs left join songsbyartist using(id_song) left join artists using(id_artist) where lower(artist_name) like "%garf%" limit 10;

select song_name,release_date_year,artist_name from songs left join songsbyartist using(id_song) left join artists using(id_artist) where release_date_year between 2000 and 2018 limit 10;

select song_name,release_date_year,artist_name from songs left join songsbyartist using(id_song) left join artists using(id_artist) where lower(song_name) like "%sound%" and  lower(artist_name) like "%garf%" and release_date_year between 1900 and 2018 limit 10;

select song_name,release_date_year,artist_name 
                                  from songs left join songsbyartist using(id_song) left join artists using(id_artist) 
                                 where lower(song_name) like "%a%" 
                                 and release_date_year between 1990 and 2015 
                                  and lower(artist_name) like "%b%" limit 10;