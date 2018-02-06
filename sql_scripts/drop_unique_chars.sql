SELECT count(*) FROM artists WHERE artist_name REGEXP '^[A-Za-z0-9~!@#$%^&*()-+{},._;\t\n\w"`\'\|/ ]+$';
SELECT count(*) FROM artists ;

SELECT count(*) FROM songs WHERE song_name REGEXP '^[A-Za-z0-9~!@#$%^&*()-+{},._;\t\n\w"`\'\|/ ]+$';
SELECT count(*) FROM songs ;


SELECT count(*) FROM area WHERE area_name REGEXP '^[A-Za-z0-9~!@#$%^&*()-+{},._;\t\n\w"`\'\|/ ]+$';
SELECT count(*) FROM area ;

SELECT count(*) FROM genres WHERE genere_name REGEXP '^[A-Za-z0-9~!@#$%^&*()-+{},._;\t\n\w"`\'\|/ ]+$';
SELECT count(*) FROM genres ;

delete FROM artists WHERE area_name not REGEXP '^[A-Za-z0-9~!@#$%^&*()-+{},._;\t\n\w"`\'\|/ ]+$';


select distinct id_artist,artist_name,song_name,begin_date_day,begin_date_month,begin_date_year from (select id_song,song_name from songs where lower(song_name) like '% comarovo%') as t 
                                      left join songsbyartist using (id_song) left join artists using (id_artist) where begin_date_year between 0 and 9999 limit 10; 

