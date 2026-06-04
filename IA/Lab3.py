import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
from sklearn.metrics import classification_report, confusion_matrix, accuracy_score

# Importăm algoritmii din cele 4 categorii cerute în laborator
from sklearn.naive_bayes import GaussianNB          # 1. Bazat pe Bayes
from sklearn.svm import SVC                         # 2. Bazat pe Funcții
from sklearn.tree import DecisionTreeClassifier     # 3. Bazat pe Arbori
from sklearn.ensemble import RandomForestClassifier # 4. Meta-metode

print("="*60)
print(" LABORATOR 3: TEHNICI DE ÎNVĂȚARE AUTOMATĂ SUPERVIZATĂ")
print(" Set de date: Diabet (Kaggle)")
print("="*60)

# --- PASUL 1: CITIREA DATELOR ---
print("\n[1] Se descarcă setul de date...")
url_diabetes = "https://raw.githubusercontent.com/susanli2016/Machine-Learning-with-Python/master/diabetes.csv"
df = pd.read_csv(url_diabetes)

# --- PASUL 2: ANALIZA EXPLORATORIE (EDA) ---
print("\n[2] Analiza Exploratorie a Datelor (EDA)...")
# Grafic 1: Verificare date lipsă
plt.figure(figsize=(8, 5))
sns.heatmap(df.isnull(), yticklabels=False, cbar=False, cmap='viridis')
plt.title("EDA 1: Verificare date lipsă (Dacă e complet mov, nu sunt valori nule)")
plt.show()

# Grafic 2: Echilibrul claselor (Sănătos vs Diabet)
plt.figure(figsize=(6, 4))
sns.countplot(x='Outcome', data=df, palette='RdBu_r')
plt.title("EDA 2: Distribuția claselor (0 = Sănătos, 1 = Diabet)")
plt.show()

# --- PASUL 3: PREPROCESARE ȘI NORMALIZARE ---
print("\n[3] Preprocesarea și Normalizarea datelor...")
X = df.drop('Outcome', axis=1) # Caracteristicile (vârstă, insulină, glucoză etc.)
y = df['Outcome']              # Eticheta (0 sau 1)

# Divizarea datelor (80% antrenament, 20% testare - conform PDF)
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.20, random_state=42)

# Normalizarea (Scalarea) datelor - Foarte importantă pentru SVM!
scaler = StandardScaler()
X_train_scaled = scaler.fit_transform(X_train)
X_test_scaled = scaler.transform(X_test)

# --- PASUL 4: IMPLEMENTAREA ALGORITMILOR ---
# Definim un dicționar cu cele 4 modele alese
modele = {
    "1. Naive Bayes (GaussianNB)": GaussianNB(),
    "2. Support Vector Machine (SVC)": SVC(kernel='linear', random_state=42),
    "3. Arbore de Decizie (Decision Tree)": DecisionTreeClassifier(random_state=42),
    "4. Random Forest (Meta-metodă)": RandomForestClassifier(n_estimators=100, random_state=42)
}

rezultate_acuratete = {}

print("\n[4] Antrenarea și Evaluarea Modelelor...")
for nume_model, model in modele.items():
    print(f"\n{'='*50}\n Model: {nume_model}\n{'='*50}")
    
    # Antrenare
    model.fit(X_train_scaled, y_train)
    
    # Predicție
    predictii = model.predict(X_test_scaled)
    
    # Salvare acuratețe pentru comparația finală
    acc = accuracy_score(y_test, predictii)
    rezultate_acuratete[nume_model] = acc
    
    # Afișare Raport de Clasificare
    print("Raport de Clasificare:")
    print(classification_report(y_test, predictii))
    
    # Afișare Matrice de Confuzie (Grafic - similar cu Fig 3.1 din PDF)
    cm = confusion_matrix(y_test, predictii)
    plt.figure(figsize=(5, 4))
    sns.heatmap(cm, annot=True, fmt='d', cmap='Blues', cbar=False)
    plt.title(f"Matrice de Confuzie: {nume_model}")
    plt.xlabel('Prezis')
    plt.ylabel('Real')
    plt.show()

# --- PASUL 5: COMPARAȚIA FINALĂ ---
print("\n[5] Comparația Acurateții Modelelor:")
for nume, acc in rezultate_acuratete.items():
    print(f"{nume}: {acc * 100:.2f}%")

# --- PASUL 6: TESTARE CU DATE NOI (INTRODUSE MANUAL) ---
print("\n[6] Testarea modelului Random Forest cu date noi (Exemplu pacient nou)...")
# Creăm un pacient fals (Pregnancies, Glucose, BloodPressure, SkinThickness, Insulin, BMI, DiabetesPedigreeFunction, Age)
pacient_nou = np.array([[2, 140, 70, 25, 100, 28.5, 0.5, 35]])

# IMPORTANT: Datele noi trebuie normalizate cu același scaler!
pacient_nou_scaled = scaler.transform(pacient_nou)

# Folosim modelul Random Forest (de obicei cel mai performant) pentru predicție
model_rf = modele["4. Random Forest (Meta-metodă)"]
predictie_noua = model_rf.predict(pacient_nou_scaled)

print("Date pacient nou:", pacient_nou[0])
if predictie_noua[0] == 1:
    print("=> REZULTAT PREZIS: Pacientul este suspect de DIABET (Clasa 1).")
else:
    print("=> REZULTAT PREZIS: Pacientul este SĂNĂTOS (Clasa 0).")