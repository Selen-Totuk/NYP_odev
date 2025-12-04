using System;

public abstract class KuantumNesnesi
{
    // Özellikler (Properties)
    public string ID { get; private set; }
    public int TehlikeSeviyesi { get; private set; }

    private double _stabilite;

    // Kapsülleme (Encapsulation): 0-100 aralığı kontrolü
    public double Stabilite
    {
        get { return _stabilite; }
        protected set // Stabilite sadece alt sınıflar ve iç metotlarca değiştirilebilir.
        {
            if (value > 100)
                _stabilite = 100;
            else if (value <= 0)
                _stabilite = 0; // 0'a eşit veya altı kontrolü için
            else
                _stabilite = value;
        }
    }

    // Constructor (Yapıcı Metot)
    public KuantumNesnesi(string id, int tehlikeSeviyesi, double baslangicStabilite)
    {
        ID = id;
        TehlikeSeviyesi = tehlikeSeviyesi;
        Stabilite = baslangicStabilite;
    }

    // Soyut Metot: Alt sınıflar kendi AnalizEt() mantığını uygulamak zorundadır.
    public abstract void AnalizEt();

    // Durum Bilgisi Metodu (Polimorfizm için kullanılacak)
    public string DurumBilgisi()
    {
        // F2: Virgülden sonra 2 basamak göster.
        return $"[ID: {ID}] Stabilite: {Stabilite:F2}%, Tehlike: {TehlikeSeviyesi}";
    }
}