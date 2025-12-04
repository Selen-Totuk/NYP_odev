using System;

public class VeriPaketi : KuantumNesnesi
{
    // Başlangıç değerleri (ID, Tehlike Seviyesi 1, Stabilite 80)
    public VeriPaketi(string id) : base(id, 1, 80) { }

    public override void AnalizEt()
    {
        // Kritik kontrol: Düşüş sonrası stabilite 0'ın altına düşecek mi?
        if (Stabilite - 5 <= 0)
        {
            throw new KuantumCokusuException(this.ID);
        }

        Stabilite -= 5;
        Console.WriteLine($"[VeriPaketi - {ID}] Veri içeriği okundu. Stabilite -5.");
    }
}