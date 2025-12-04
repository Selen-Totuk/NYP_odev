# exceptions.py
class KuantumCokusuException(Exception):
    """Stabilite 0'ın altına düştüğünde fırlatılan özel hata sınıfı."""
    def __init__(self, nesne_id):
        self.nesne_id = nesne_id
        super().__init__(f"Kuantum Çöküşü! Patlayan Nesne ID: {nesne_id}")

# IKritik arayüzü kontrolü için boş bir sınıf (Duck Typing'i kolaylaştırmak için)
class IKritik:
    """AcilDurumSogutmasi metodu içermesi gereken nesneler için bir işaretleyici."""
    pass