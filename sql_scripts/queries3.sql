select count(*) as num,genere_name from (select id_artist from favoriteartistbyuser where id_user = 1) as t left join genresbyartist using(id_artist) left join genres using (id_genre) where genere_name is not null and lower(genere_name) like '%goth%'  group by id_genre limit 1;

select count(*) as num,genere_name from (select id_artist from favoriteartistbyuser where id_user = 1) as t left join genresbyartist using(id_artist) left join genres using (id_genre) where genere_name is not null and genere_name = "'80s" group by id_genre limit 1;