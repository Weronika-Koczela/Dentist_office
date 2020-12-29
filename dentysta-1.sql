-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 28 Gru 2020, 21:45
-- Wersja serwera: 10.4.11-MariaDB
-- Wersja PHP: 7.4.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `dentysta`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `kalendarz`
--

CREATE TABLE `kalendarz` (
  `ID_terminu` int(11) NOT NULL,
  `Data` date NOT NULL,
  `Godzina` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_polish_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `karta_pacjenta`
--

CREATE TABLE `karta_pacjenta` (
  `Id_karty` int(11) NOT NULL,
  `Id_uzebienie` int(11) NOT NULL,
  `Id_pacjenta` int(11) NOT NULL,
  `Id_lekarza` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_polish_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `lista_zebow`
--

CREATE TABLE `lista_zebow` (
  `Id_uzebienie` int(11) NOT NULL,
  `Id_zeba` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_polish_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `uzytkownik`
--

CREATE TABLE `uzytkownik` (
  `ID_uzytkownika` int(11) NOT NULL,
  `Imie` varchar(50) COLLATE ucs2_polish_ci NOT NULL,
  `Nazwisko` varchar(50) COLLATE ucs2_polish_ci NOT NULL,
  `PESEL` varchar(11) COLLATE ucs2_polish_ci NOT NULL,
  `Haslo` varchar(100) COLLATE ucs2_polish_ci NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_polish_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `wizyta`
--

CREATE TABLE `wizyta` (
  `ID_Wizyty` int(11) NOT NULL,
  `ID_lekarza` int(11) NOT NULL,
  `ID_terminu` int(11) NOT NULL,
  `ID_Pacjenta` int(11) NOT NULL,
  `ID_rejestratora` int(11) NOT NULL,
  `Status_wizyty` varchar(20) COLLATE ucs2_polish_ci NOT NULL,
  `Czas trwania` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_polish_ci;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `zeby`
--

CREATE TABLE `zeby` (
  `ID_zeba` int(11) NOT NULL,
  `Oznaczenie` varchar(6) COLLATE ucs2_polish_ci NOT NULL,
  `Stan` varchar(10) COLLATE ucs2_polish_ci NOT NULL,
  `Opis` text COLLATE ucs2_polish_ci NOT NULL,
  `Data_aktualizacji` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=ucs2 COLLATE=ucs2_polish_ci;

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `kalendarz`
--
ALTER TABLE `kalendarz`
  ADD PRIMARY KEY (`ID_terminu`);

--
-- Indeksy dla tabeli `karta_pacjenta`
--
ALTER TABLE `karta_pacjenta`
  ADD PRIMARY KEY (`Id_karty`);

--
-- Indeksy dla tabeli `lista_zebow`
--
ALTER TABLE `lista_zebow`
  ADD PRIMARY KEY (`Id_uzebienie`);

--
-- Indeksy dla tabeli `uzytkownik`
--
ALTER TABLE `uzytkownik`
  ADD PRIMARY KEY (`ID_uzytkownika`);

--
-- Indeksy dla tabeli `wizyta`
--
ALTER TABLE `wizyta`
  ADD PRIMARY KEY (`ID_Wizyty`);

--
-- Indeksy dla tabeli `zeby`
--
ALTER TABLE `zeby`
  ADD PRIMARY KEY (`ID_zeba`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT dla tabeli `kalendarz`
--
ALTER TABLE `kalendarz`
  MODIFY `ID_terminu` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `karta_pacjenta`
--
ALTER TABLE `karta_pacjenta`
  MODIFY `Id_karty` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `uzytkownik`
--
ALTER TABLE `uzytkownik`
  MODIFY `ID_uzytkownika` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
