import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.datasets import make_blobs, make_moons
from sklearn.preprocessing import StandardScaler
from sklearn.metrics import silhouette_score

# Importăm algoritmii din cele 4 categorii
from sklearn.cluster import KMeans                     # 1. Partițional
from sklearn.cluster import AgglomerativeClustering    # 2. Ierarhic
from sklearn.cluster import DBSCAN                     # 3. Bazat pe densitate
from sklearn.mixture import GaussianMixture            # 4. Probabilistic

print("="*60)
print(" LABORATOR 4: CLUSTERIZARE (Învățare Nesupervizată)")
print("="*60)

# --- 1. GENERAREA DATELOR ---
# Creăm două tipuri de date pentru a vedea cum se comportă algoritmii
# Dataset 1: Grupuri globulare (perfecte pentru K-Means, GMM, Ierarhic)
X_blobs, _ = make_blobs(n_samples=300, centers=3, cluster_std=0.8, random_state=42)

# Dataset 2: Forme complexe/Semiluni (perfecte pentru DBSCAN)
X_moons, _ = make_moons(n_samples=300, noise=0.05, random_state=42)

# Normalizarea datelor (Obligatorie pentru clustering pe bază de distanțe)
scaler = StandardScaler()
X_blobs_scaled = scaler.fit_transform(X_blobs)
X_moons_scaled = scaler.fit_transform(X_moons)

# --- 2. CONFIGURAREA ALGORITMILOR ---
# Vom aplica primele 3 metode pe Dataset 1 (Blobs) și DBSCAN pe Dataset 2 (Moons)
modele = {
    "K-Means (Partițional)": (KMeans(n_clusters=3, random_state=42), X_blobs_scaled),
    "Agglomerative (Ierarhic)": (AgglomerativeClustering(n_clusters=3), X_blobs_scaled),
    "Gaussian Mixture (Probabilistic)": (GaussianMixture(n_components=3, random_state=42), X_blobs_scaled),
    "DBSCAN (Densitate)": (DBSCAN(eps=0.3, min_samples=5), X_moons_scaled)
}

# --- 3. IMPLEMENTARE, EVALUARE ȘI VIZUALIZARE ---
plt.figure(figsize=(15, 10))

for i, (nume_model, (model, date_X)) in enumerate(modele.items(), 1):
    # a) Antrenarea modelului (Atenție: Nu avem y_train! Este nesupervizat)
    if nume_model == "Gaussian Mixture (Probabilistic)":
        model.fit(date_X)
        predictii = model.predict(date_X)
    else:
        predictii = model.fit_predict(date_X)
    
    # b) Evaluarea prin Silhouette Score (măsoară compactitatea clusterelor)
    # Ignorăm zgomotul (-1) din DBSCAN pentru scorul siluetei
    scor_silueta = "N/A"
    if len(np.unique(predictii[predictii != -1])) > 1:
        scor_silueta = round(silhouette_score(date_X[predictii != -1], predictii[predictii != -1]), 3)
    
    print(f"Model: {nume_model} | Scorul Siluetei: {scor_silueta}")
    
    # c) Trasarea Graficelor
    plt.subplot(2, 2, i)
    # Folosim o paletă de culori. Zgomotul (-1) va fi negru.
    sns.scatterplot(x=date_X[:, 0], y=date_X[:, 1], hue=predictii, palette="deep", legend=False)
    plt.title(f"{nume_model}\nSilhouette Score: {scor_silueta}")
    plt.xlabel("Feature 1")
    plt.ylabel("Feature 2")

plt.tight_layout()
plt.show()

print("\n[INFO] Rularea s-a terminat. Observați cum DBSCAN rezolvă corect formele complexe!")