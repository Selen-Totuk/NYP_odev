using System;

public class KaranlikMadde : KuantumNesnesi, IKritik
{
    // Başlangıç değerleri (ID, Tehlike Seviyesi 7, Stabilite 60)
    public KaranlikMadde(string id) : base(id, 7, 60) { }

    public override void AnalizEt()
    {
        // Kritik kontrol: Düşüş sonrası stabilite 0'ın altına düşecek mi?
        if (Stabilite - 15 <= 0)
        {
            throw new KuantumCokusuException(this.ID);
        }

        Stabilite -= 15;
        Console.WriteLine($"[KaranlikMadde - {ID}] Radyasyon seviyesi yükseldi. Stabilite -15.");
    }

    // IKritik arayüz metodu uygulaması
    public void AcilDurumSogutmasi()
    {
        // Stabilite setter'ı sayesinde 100'ü geçmeyecek
        Stabilite += 50;
        Console.WriteLine($"[KaranlikMadde - {ID}] Acil Soğutma Başarılı! Stabilite +50.");
    }
}