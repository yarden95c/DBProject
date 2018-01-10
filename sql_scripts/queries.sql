use music_fact_generator;




use music_fact_generator;
select song_name,release_date_year from songs where lower(song_name) like '%round%';

create view song_view as
select song_name,release_date_year,artist_name from songs left join songsbyartist as t using(id_song) left join artists using(id_artist);


-- song
select song_name,release_date_year,artist_name from songs left join songsbyartist using(id_song) left join artists using(id_artist)
 where lower(song_name) like '%round%'
 and release_date_year between 2000 and 2017
 and lower(artist_name) like '%o%' limit 10;
 
 
 
 -- artist
 select artist_name,song_name,begin_date_day,begin_date_month,begin_date_year from artists left join songsbyartist using(id_artist) left join songs using(id_song)
 where lower(artist_name) like '%o%'
 and lower(song_name) like '%a%'
 and begin_date_year between 1900 and 2017 limit 10;
 
 
 ALTER TABLE `music_fact_generator`.`artists` 
CHANGE COLUMN `area_Id` `id_area` INT(8) NULL DEFAULT NULL ;