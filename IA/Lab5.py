# ==========================================
# 1. IMPORTAREA LIBRĂRIILOR
# ==========================================
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler, LabelEncoder
from sklearn.metrics import accuracy_score, confusion_matrix, classification_report
from sklearn.utils.class_weight import compute_class_weight
from sklearn.neural_network import MLPClassifier  # ← înlocuiește TensorFlow/Keras

# ==========================================
# 2. CITIREA DATELOR
# ==========================================
print("[INFO] Se descarcă setul de date Diabet...")
url = "https://raw.githubusercontent.com/susanli2016/Machine-Learning-with-Python/master/diabetes.csv"
data = pd.read_csv(url)

print("\n--- PASUL 3: Date inițiale ---")
print(data.head())
data.info()
print(data.describe())

# ==========================================
# 3. EDA
# ==========================================
print("\n--- PASUL 5: Valori Nule ---")
print(data.isnull().sum())

# ==========================================
# 4. PREGĂTIREA DATELOR
# ==========================================
X = data.drop('Outcome', axis=1)
y = data['Outcome']

X_train, X_test, y_train, y_test = train_test_split(
    X, y, test_size=0.3, random_state=101
)

sc = StandardScaler()
X_train = sc.fit_transform(X_train)
X_test  = sc.transform(X_test)

# ==========================================
# 5. MODELUL INIȚIAL (echivalent model_1)
# ==========================================
# hidden_layer_sizes=(16, 8) = 2 straturi: 16 neuroni, 8 neuroni
model_1 = MLPClassifier(
    hidden_layer_sizes=(16, 8),
    activation='relu',
    solver='adam',
    max_iter=200,
    random_state=42
)
model_1.fit(X_train, y_train)

pred_1 = model_1.predict(X_test)

print("\n--- Rezultate Model Inițial ---")
print("Acuratețe:", accuracy_score(y_test, pred_1))
print("\nMatrice de Confuzie:\n", confusion_matrix(y_test, pred_1))
print("\nRaport de Clasificare:\n", classification_report(y_test, pred_1))

# Grafic loss (echivalent history.history['loss'])
plt.figure(figsize=(8, 4))
plt.plot(model_1.loss_curve_)
plt.title('Evoluția Loss (Model Inițial)')
plt.xlabel('Iterații')
plt.ylabel('Loss')
plt.tight_layout()
plt.show()

# ==========================================
# 6. PONDERI DE CLASĂ (date dezechilibrate)
# ==========================================
print("\n--- Date Dezechilibrate ---")
print(pd.Series(y_train).value_counts())

class_weights_array = compute_class_weight(
    class_weight='balanced',
    classes=np.unique(y_train),
    y=y_train
)
class_weights_dict = {i: w for i, w in enumerate(class_weights_array)}
print("Ponderi clase:", class_weights_dict)

model_weighted = MLPClassifier(
    hidden_layer_sizes=(16, 8),
    activation='relu',
    solver='adam',
    max_iter=200,
    random_state=42
)
model_weighted.fit(X_train, y_train, sample_weight=[
    class_weights_dict[yi] for yi in y_train
])

pred_w = model_weighted.predict(X_test)
print("\nRaport (Cu Ponderi):\n", classification_report(y_test, pred_w))

# ==========================================
# 7. MODELUL OPTIMIZAT (echivalent model_opt)
# ==========================================
# hidden_layer_sizes=(32, 16) = mai mulți neuroni, strat în plus
# alpha = regularizare L2 (echivalent Dropout)
# early_stopping = oprire automată dacă nu se îmbunătățește
model_opt = MLPClassifier(
    hidden_layer_sizes=(32, 16),
    activation='relu',
    solver='adam',
    alpha=0.01,           # regularizare L2 (înlocuiește Dropout)
    early_stopping=True,  # echivalent EarlyStopping callback
    validation_fraction=0.1,
    max_iter=500,
    random_state=42
)
model_opt.fit(X_train, y_train, sample_weight=[
    class_weights_dict[yi] for yi in y_train
])

pred_opt = model_opt.predict(X_test)

print("\n--- Rezultate Model Optimizat ---")
print("Acuratețe:", accuracy_score(y_test, pred_opt))
print("\nMatrice de Confuzie:\n", confusion_matrix(y_test, pred_opt))
print("\nRaport de Clasificare:\n", classification_report(y_test, pred_opt))

# Grafice finale
plt.figure(figsize=(12, 4))
plt.subplot(1, 2, 1)
plt.plot(model_opt.loss_curve_, label='Train Loss')
if model_opt.best_loss_ is not None:
    plt.axhline(model_opt.best_loss_, color='orange', linestyle='--', label='Best Loss')
plt.title('Loss (Model Optimizat)')
plt.xlabel('Iterații')
plt.legend()

plt.subplot(1, 2, 2)
cm = confusion_matrix(y_test, pred_opt)
sns.heatmap(cm, annot=True, fmt='d', cmap='Blues',
            xticklabels=['Sănătos', 'Diabet'],
            yticklabels=['Sănătos', 'Diabet'])
plt.title('Matrice de Confuzie')
plt.tight_layout()
plt.show()

print("\n--- CONCLUZII ---")
print("Modelul optimizat folosește regularizare L2 (alpha) în loc de Dropout,")
print("și early_stopping în loc de callback-uri Keras. Rezultatele sunt similare.")