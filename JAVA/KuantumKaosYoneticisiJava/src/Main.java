import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.Scanner;
import exceptions.KuantumCokusuException;
import interfaces.IKritik;
import models.AntiMadde;
import models.KaranlikMadde;
import models.KuantumNesnesi;
import models.VeriPaketi;

public class Main {
    // Generic List: List<KuantumNesnesi> kullanılır.
    private static List<KuantumNesnesi> envanter = new ArrayList<>();
    private static Random random = new Random();
    private static int nesneSayaci = 1;

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        boolean gameRunning = true;

        while (gameRunning) {
            try {
                displayMenu();
                
                if (scanner.hasNextInt()) {
                    int secim = scanner.nextInt();
                    scanner.nextLine(); // Satır sonunu tüket

                    switch (secim) {
                        case 1: yeniNesneEkle(); break;
                        case 2: tumEnvanteriListele(); break;
                        case 3: nesneyiAnalizEt(scanner); break;
                        case 4: acilDurumSogutmasiYap(scanner); break;
                        case 5:
                            gameRunning = false;
                            System.out.println("Program sonlandırılıyor...");
                            break;
                        default:
                            System.out.println("Geçersiz seçim. Lütfen 1-5 arasında bir rakam girin.");
                            break;
                    }
                } else {
                    System.out.println("Geçersiz giriş. Lütfen bir sayı girin.");
                    scanner.nextLine();
                }

            } 
            // Game Over Kontrolü: try-catch ile KuantumCokusuException yakalanır.
            catch (KuantumCokusuException ex) {
                System.out.println("\n**************************************************");
                System.out.println("!!!!!!!!!!!! SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR... !!!!!!!!!!!!");
                System.out.println("Hata: " + ex.getMessage());
                System.out.println("**************************************************");
                gameRunning = false;
            } 
            catch (Exception ex) {
                System.out.println("Beklenmedik bir hata oluştu: " + ex.getMessage());
            }
        }
        scanner.close();
    }

    private static void displayMenu() {
        System.out.println("\n--- KUANTUM AMBARI KONTROL PANELİ ---");
        System.out.println("1. Yeni Nesne Ekle (Rastgele)");
        System.out.println("2. Tüm Envanteri Listele (Durum Raporu)");
        System.out.println("3. Nesneyi Analiz Et (ID isteyerek)");
        System.out.println("4. Acil Durum Soğutması Yap");
        System.out.println("5. Çıkış");
        System.out.print("Seçiminiz: ");
    }

    private static void yeniNesneEkle() {
        String id = String.format("QN-%03d", nesneSayaci++);
        KuantumNesnesi yeniNesne;

        int tip = random.nextInt(3); // 0:VeriPaketi, 1:KaranlikMadde, 2:AntiMadde
        
        switch (tip) {
            case 0: yeniNesne = new VeriPaketi(id); break;
            case 1: yeniNesne = new KaranlikMadde(id); break;
            case 2: yeniNesne = new AntiMadde(id); break;
            default: return;
        }

        envanter.add(yeniNesne);
        System.out.println("\n[BAŞARILI] Yeni nesne eklendi: " + yeniNesne.getClass().getSimpleName() + " - ID: " + id);
    }

    private static void tumEnvanteriListele() {
        if (envanter.isEmpty()) {
            System.out.println("Envanterde hiç nesne yok.");
            return;
        }

        System.out.println("\n--- ENVANTER DURUM RAPORU ---");
        // Polimorfizm: Tüm nesneler DurumBilgisi() metodunu çağırır.
        for (KuantumNesnesi nesne : envanter) {
            System.out.println(nesne.DurumBilgisi() + " (Tür: " + nesne.getClass().getSimpleName() + ")");
        }
    }

    private static void nesneyiAnalizEt(Scanner scanner) {
        System.out.print("Analiz edilecek nesnenin ID'sini girin: ");
        String id = scanner.nextLine().trim();

        KuantumNesnesi hedef = envanter.stream()
                .filter(n -> n.getID().equalsIgnoreCase(id))
                .findFirst()
                .orElse(null);

        if (hedef != null) {
            // Polimorfizm: Nesnenin AnalizEt() metodu çağrılır.
            hedef.AnalizEt();
            System.out.println("Analiz sonrası durum: " + hedef.DurumBilgisi());
        } else {
            System.out.println("HATA: Belirtilen ID'ye sahip nesne bulunamadı.");
        }
    }

    private static void acilDurumSogutmasiYap(Scanner scanner) {
        System.out.print("Soğutulacak nesnenin ID'sini girin: ");
        String id = scanner.nextLine().trim();

        KuantumNesnesi hedef = envanter.stream()
                .filter(n -> n.getID().equalsIgnoreCase(id))
                .findFirst()
                .orElse(null);

        if (hedef == null) {
            System.out.println("HATA: Belirtilen ID'ye sahip nesne bulunamadı.");
            return;
        }

        // Type Checking (instanceof): Nesnenin IKritik arayüzünü uygulayıp uygulamadığı kontrol edilir.
        if (hedef instanceof IKritik) {
            // Hedef nesneyi IKritik tipine dönüştür (cast)
            IKritik kritikNesne = (IKritik) hedef; 
            kritikNesne.AcilDurumSogutmasi();
            System.out.println("Soğutma sonrası durum: " + hedef.DurumBilgisi());
        } else {
            // VeriPaketi (IKritik olmayan) için hata mesajı
            System.out.println("HATA: Bu nesne (VeriPaketi) kritik bir nesne değildir ve soğutulamaz!");
        }
    }
}