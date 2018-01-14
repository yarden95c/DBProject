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
 
 
 
select id_artist,artist_name,begin_date_day,begin_date_month,begin_date_year from artists
where lower(artist_name) like '%%'
and begin_date_year between 0 and 9999;

select song_name from songsbyartist left join songs using(id_song) where song_name like '%%' and id_artist = '25';
 
 
 -- place
select area_name,IFNULL(artist_name,"") from area left join artists using(id_area)
where lower(area_name) like "%A Arnoia%"
and lower(artist_name) like "%%" order by area_name;


select area_name,artist_name from (select area_name,IFNULL(artist_name,"") as artist_name from area left join artists using(id_area)) as t
where lower(area_name) like "%A Arnoia%"
and lower(artist_name) like "%%" order by area_name;


select area_name as area_name,IFNULL(artist_name,"") as artist_name from area left join artists using(id_area);


