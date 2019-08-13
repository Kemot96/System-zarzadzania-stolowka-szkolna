using System;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class RokMetadata
    {
        [Required]
        [Range(1950, 2100)]
        [Display(Name = "Rok")]
        public string rok1;
    }

    public class PosilekMetadata
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Nazwa")]
        public string nazwa;

        [Required]
        [Display(Name = "Waga(w gramach)")]
        public int waga;
    }

    public class JadlospisMetadata
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Od")]
        public DateTime od;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Do")]
        public DateTime @do;
    }

    public class MiesiacMetadata
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "Nazwa")]
        public string nazwa;
    }

    public class KontoMetadata
    {
        [Required]
        [MaxLength(20)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Login")]
        public string login;

        [Required]
        [MaxLength(20)]
        [Display(Name = "Hasło")]
        public string haslo;
    }

    public class AdresMetadata
    {
        [Required]
        [MaxLength(30)]
        [Display(Name = "Miasto")]
        public string miasto;

        [Required]
        [MinLength(6)]
        [MaxLength(6)]
        [Display(Name = "Kod pocztowy")]
        public string kod;

        [Required]
        [MaxLength(50)]
        [Display(Name = "Ulica")]
        public string ulica;

        [Required]
        [MaxLength(10)]
        [Display(Name = "Numer")]
        public string numer;
    }

    public class FirmaMetadata
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Nazwa")]
        public string nazwa;
    }

    public class PracownikMetadata
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "Imię")]
        public string imie;

        [Required]
        [MaxLength(20)]
        [Display(Name = "Nazwisko")]
        public string nazwisko;

        [Required]
        [MaxLength(20)]
        [Display(Name = "Stanowisko")]
        public string stanowisko;
    }

    public class DzieckoMetadata
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "Imię")]
        public string imie;

        [Required]
        [MaxLength(20)]
        [Display(Name = "Nazwisko")]
        public string nazwisko;

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        [Display(Name = "Klasa")]
        public string klasa;
    }

    public class NieobecnoscMetadata
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Od")]
        public DateTime od;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Do")]
        public DateTime @do;
    }

    public class OplataMetadata
    {
        [Required]
        [Display(Name = "Stawka za dzien(w zł)")]
        public float stawka_za_dzien;

        [Required]
        [Range(0, 31)]
        [Display(Name = "Dni w miesiacu")]
        public int dni_w_miesiacu;
    }

    public class TransakcjaMetadata
    {
        [Required]
        [Display(Name = "Kwota(w zł)")]
        public float kwota;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data zaplaty")]
        public DateTime data_zaplaty;
    }
}