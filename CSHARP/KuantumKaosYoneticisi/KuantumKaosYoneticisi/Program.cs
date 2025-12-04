using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    // Generic List: Tüm nesneleri List<KuantumNesnesi> olarak tutar.
    private static List<KuantumNesnesi> envanter = new List<KuantumNesnesi>();
    private static Random random = new Random();
    private static int nesneSayaci = 1;

    public static void Main(string[] args)
    {
        bool gameRunning = true;

        while (gameRunning)
        {
            try
            {
                // Menü Gösterimi
                Console.WriteLine("\n--- KUANTUM AMBARI KONTROL PANELİ ---");
                Console.WriteLine("1. Yeni Nesne Ekle (Rastgele Veri/Karanlık Madde/Anti Madde üretir)");
                Console.WriteLine("2. Tüm Envanteri Listele (Durum Raporu)");
                Console.WriteLine("3. Nesneyi Analiz Et (ID isteyerek)");
                Console.WriteLine("4. Acil Durum Soğutması Yap (Sadece IKritik olanlar için!)");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");

                if (int.TryParse(Console.ReadLine(), out int secim))
                {
                    switch (secim)
                    {
                        case 1: YeniNesneEkle(); break;
                        case 2: TümEnvanteriListele(); break;
                        case 3: NesneyiAnalizEt(); break;
                        case 4: AcilDurumSogutmasiYap(); break;
                        case 5:
                            gameRunning = false;
                            Console.WriteLine("Program sonlandırılıyor...");
                            break;
                        default: Console.WriteLine("Geçersiz seçim. Lütfen 1-5 arasında bir rakam girin."); break;
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz giriş.");
                }

            }
            // Game Over: KuantumCokusuException yakalanırsa program sonlanır.
            catch (KuantumCokusuException ex)
            {
                Console.WriteLine("\n**************************************************");
                Console.WriteLine("!!!!!!!!!!!! SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR... !!!!!!!!!!!!");
                Console.WriteLine($"Hata: {ex.Message}");
                Console.WriteLine("**************************************************");
                gameRunning = false; // Döngüyü sonlandır
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Beklenmedik bir hata oluştu: {ex.Message}");
            }
        }
    }

    private static void YeniNesneEkle()
    {
        string id = $"QN-{nesneSayaci++:D3}";
        KuantumNesnesi yeniNesne = null;

        int tip = random.Next(1, 4); // 1: Veri, 2: Karanlık, 3: Anti

        switch (tip)
        {
            case 1: yeniNesne = new VeriPaketi(id); break;
            case 2: yeniNesne = new KaranlikMadde(id); break;
            case 3: yeniNesne = new AntiMadde(id); break;
        }

        envanter.Add(yeniNesne);
        Console.WriteLine($"\n[BAŞARILI] Yeni nesne eklendi: {yeniNesne.GetType().Name} - ID: {id}");
    }

    private static void TümEnvanteriListele()
    {
        if (envanter.Count == 0)
        {
            Console.WriteLine("Envanterde hiç nesne yok.");
            return;
        }

        Console.WriteLine("\n--- ENVANTER DURUM RAPORU ---");
        // Polimorfizm: Tüm nesneler DurumBilgisi() metodunu çağırır.
        foreach (var nesne in envanter)
        {
            Console.WriteLine(nesne.DurumBilgisi() + $" (Tür: {nesne.GetType().Name})");
        }
    }

    private static void NesneyiAnalizEt()
    {
        Console.Write("Analiz edilecek nesnenin ID'sini girin: ");
        string id = Console.ReadLine().Trim();

        // Linq ile nesneyi bulma
        KuantumNesnesi hedef = envanter.FirstOrDefault(n => n.ID.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (hedef != null)
        {
            // Polimorfizm: Nesnenin gerçek tipine göre AnalizEt() metodu çalışır.
            hedef.AnalizEt();
            Console.WriteLine($"Analiz sonrası durum: {hedef.DurumBilgisi()}");
        }
        else
        {
            Console.WriteLine("HATA: Belirtilen ID'ye sahip nesne bulunamadı.");
        }
    }

    private static void AcilDurumSogutmasiYap()
    {
        Console.Write("Soğutulacak nesnenin ID'sini girin: ");
        string id = Console.ReadLine().Trim();

        KuantumNesnesi hedef = envanter.FirstOrDefault(n => n.ID.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (hedef == null)
        {
            Console.WriteLine("HATA: Belirtilen ID'ye sahip nesne bulunamadı.");
            return;
        }

        // Type Checking (is anahtar kelimesi): Nesnenin IKritik olup olmadığı kontrol edilir.
        if (hedef is IKritik kritikNesne)
        {
            // IKritik ise AcilDurumSogutmasi() metodunu çağır.
            kritikNesne.AcilDurumSogutmasi();
            Console.WriteLine($"Soğutma sonrası durum: {hedef.DurumBilgisi()}");
        }
        else
        {
            // VeriPaketi için hata mesajı
            Console.WriteLine("HATA: Bu nesne (VeriPaketi) kritik bir nesne değildir ve soğutulamaz!");
        }
    }
}