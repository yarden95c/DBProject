CREATE DATABASE  IF NOT EXISTS `facts_generator_final` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `facts_generator_final`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: music_fact_generator
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `area`
--

DROP TABLE IF EXISTS `area`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `area` (
  `id_area` int(11) NOT NULL,
  `area_name` varchar(100) DEFAULT NULL,
  `area_type` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_area`),
  KEY `area_name` (`area_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;


--
-- Table structure for table `artists`
--

DROP TABLE IF EXISTS `artists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `artists` (
  `id_artist` int(11) NOT NULL,
  `artist_name` varchar(100) DEFAULT NULL,
  `sort_name` varchar(100) DEFAULT NULL,
  `begin_date_year` int(4) DEFAULT NULL,
  `begin_date_month` int(2) DEFAULT NULL,
  `begin_date_day` int(2) DEFAULT NULL,
  `end_date_year` int(4) DEFAULT NULL,
  `end_date_month` int(2) DEFAULT NULL,
  `end_date_day` int(2) DEFAULT NULL,
  `id_area` int(8) DEFAULT NULL,
  `gender` varchar(45) DEFAULT NULL,
  `type` varchar(45) DEFAULT NULL,
  `comment` varchar(100) DEFAULT NULL,
  `num_of_hits` decimal(32,0) DEFAULT NULL,
  PRIMARY KEY (`id_artist`),
  FOREIGN KEY (`id_area`) REFERENCES area(`id_area`),
  KEY `artist_name` (`artist_name`),
  KEY `year` (`begin_date_year`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `genres`
--

DROP TABLE IF EXISTS `genres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `genres` (
  `id_genre` int(11) NOT NULL,
  `genere_name` varchar(45) NOT NULL,
  PRIMARY KEY (`id_genre`),
  KEY `genere_name` (`genere_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `songs`
--

DROP TABLE IF EXISTS `songs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `songs` (
  `release_date_year` int(11) DEFAULT NULL,
  `id_song` varchar(45) NOT NULL,
  `song_name` varchar(100) DEFAULT NULL,
  `num_of_hits` bigint(21) DEFAULT '0',
  PRIMARY KEY (`id_song`),
  KEY `song_name` (`song_name`),
  KEY `year` (`release_date_year`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `id_user` int(11) NOT NULL AUTO_INCREMENT,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `email` text NOT NULL,
  `password` text NOT NULL,
  `id_area` int(11) NOT NULL,
  `id_favorite_genre` int(11) NOT NULL,
  `create_time` text,
  `birthday_year` int(4) NOT NULL,
  `birthday_month` int(2) NOT NULL,
  `birthday_day` int(2) NOT NULL,
  PRIMARY KEY (`id_user`),
  FOREIGN KEY (`id_area`) REFERENCES area(`id_area`),
  FOREIGN KEY (`id_favorite_genre`) REFERENCES genres(`id_genre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `favoriteartistbyuser`
--

DROP TABLE IF EXISTS `favoriteartistbyuser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `favoriteartistbyuser` (
  `id_user` int(11) NOT NULL,
  `id_artist` int(11) NOT NULL,
  FOREIGN KEY (`id_artist`) REFERENCES artists(`id_artist`),
  FOREIGN KEY (`id_user`) REFERENCES users(`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `favoritesongbyuser`
--

DROP TABLE IF EXISTS `favoritesongbyuser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `favoritesongbyuser` (
  `id_user` int(11) NOT NULL,
  `id_song` varchar(45) NOT NULL,
  FOREIGN KEY (`id_song`) REFERENCES songs(`id_song`),
  FOREIGN KEY (`id_user`) REFERENCES users(`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;



--
-- Table structure for table `genresbyartist`
--

DROP TABLE IF EXISTS `genresbyartist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `genresbyartist` (
  `id_artist` int(11) NOT NULL,
  `id_genre` int(11) NOT NULL,
  FOREIGN KEY (`id_artist`) REFERENCES artists(`id_artist`),
  FOREIGN KEY (`id_genre`) REFERENCES genres(`id_genre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;



--
-- Table structure for table `songsbyartist`
--

DROP TABLE IF EXISTS `songsbyartist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `songsbyartist` (
  `id_artist` int(11) NOT NULL,
  `id_song` varchar(45) NOT NULL,
  FOREIGN KEY (`id_artist`) REFERENCES artists(`id_artist`),
  FOREIGN KEY (`id_song`) REFERENCES songs(`id_song`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;




-- Dump completed on 2018-01-16 22:13:09
