using System;

public class AntiMadde : KuantumNesnesi, IKritik
{
    // Başlangıç değerleri (ID, Tehlike Seviyesi 10, Stabilite 40)
    public AntiMadde(string id) : base(id, 10, 40) { }

    public override void AnalizEt()
    {
        // Kritik kontrol: Düşüş sonrası stabilite 0'ın altına düşecek mi?
        if (Stabilite - 25 <= 0)
        {
            throw new KuantumCokusuException(this.ID);
        }

        Stabilite -= 25;
        Console.WriteLine($"[AntiMadde - {ID}] EVRENİN DOKUSU TİTRİYOR... Stabilite -25.");
    }

    // IKritik arayüz metodu uygulaması
    public void AcilDurumSogutmasi()
    {
        // Stabilite setter'ı sayesinde 100'ü geçmeyecek
        Stabilite += 50;
        Console.WriteLine($"[AntiMadde - {ID}] KRİTİK SOĞUTMA! Stabilite +50. Başarılı.");
    }
}