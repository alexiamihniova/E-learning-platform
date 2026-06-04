import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import classification_report, confusion_matrix

print("="*60)
print(" PARTEA 1: REGRESIE LOGISTICĂ - TITANIC (Sarcina ghidată)")
print("="*60)

# 1. Citirea datelor direct de pe internet
print("[INFO] Se descarcă datele Titanic de pe internet...")
url_titanic = "https://raw.githubusercontent.com/datasciencedojo/datasets/master/titanic.csv"
train = pd.read_csv(url_titanic)

# --- SCREENSHOT 1 ---
print("\n[INFO] Se generează Graficul 1: Harta termică (Heatmap) pentru date lipsă...")
plt.figure(figsize=(8, 6))
sns.heatmap(train.isnull(), yticklabels=False, cbar=False, cmap='viridis')
plt.title("SCREENSHOT 1: Titanic - Date lipsă (Age și Cabin)")
plt.show() # Închide fereastra pentru a continua!

# --- SCREENSHOT 2 (Sunt 3 grafice în total aici) ---
print("[INFO] Se generează Graficele 2: Distribuția supraviețuitorilor...")
plt.figure(figsize=(6, 4))
sns.set_style('whitegrid')
sns.countplot(x='Survived', data=train, palette='RdBu_r')
plt.title("SCREENSHOT 2.1: Titanic - Total supraviețuitori")
plt.show()

plt.figure(figsize=(6, 4))
sns.set_style('whitegrid')
sns.countplot(x='Survived', hue='Sex', data=train, palette='RdBu_r')
plt.title("SCREENSHOT 2.2: Titanic - Supraviețuitori vs Sex")
plt.show()

plt.figure(figsize=(6, 4))
sns.set_style('whitegrid')
sns.countplot(x='Survived', hue='Pclass', data=train, palette='rainbow')
plt.title("SCREENSHOT 2.3: Titanic - Supraviețuitori vs Clasă")
plt.show()

# --- SCREENSHOT 3 ---
print("[INFO] Se generează Graficele 3: Distribuția vârstelor...")
plt.figure(figsize=(8, 5))
train['Age'].hist(bins=30, color='darkred', alpha=0.7)
plt.title("SCREENSHOT 3.1: Titanic - Histograma vârstelor")
plt.xlabel("Vârsta")
plt.show()

plt.figure(figsize=(10, 6))
sns.boxplot(x='Pclass', y='Age', data=train, palette='winter')
plt.title("SCREENSHOT 3.2: Titanic - Vârsta vs Clasa (Boxplot)")
plt.show()


# --- PREPROCESAREA DATELOR TITANIC ---
print("\n[INFO] Se curăță datele și se antrenează modelul Titanic...")
def impute_age(cols):
    Age = cols[0]
    Pclass = cols[1]
    if pd.isnull(Age):
        if Pclass == 1: return 37
        elif Pclass == 2: return 29
        else: return 24
    else: return Age

train['Age'] = train[['Age', 'Pclass']].apply(impute_age, axis=1)
train.drop('Cabin', axis=1, inplace=True)
train.dropna(inplace=True)

sex = pd.get_dummies(train['Sex'], drop_first=True)
embark = pd.get_dummies(train['Embarked'], drop_first=True)
train.drop(['Sex', 'Embarked', 'Name', 'Ticket'], axis=1, inplace=True)
train = pd.concat([train, sex, embark], axis=1)

X_train, X_test, y_train, y_test = train_test_split(train.drop('Survived', axis=1), 
                                                    train['Survived'], test_size=0.30, random_state=101)
logmodel = LogisticRegression(max_iter=1000)
logmodel.fit(X_train, y_train)
predictions = logmodel.predict(X_test)


# --- SCREENSHOT 4 (DIN CONSOLĂ) ---
print("\n" + "*"*40)
print(" 📸 FĂ SCREENSHOT LA URMĂTORUL TEXT: ")
print("*"*40)
print("\n--- SCREENSHOT 4: Rezultate Evaluare Titanic ---")
print("Matricea de confuzie:\n", confusion_matrix(y_test, predictions))
print("\nRaport de clasificare:\n", classification_report(y_test, predictions))
print("*"*40 + "\n")



print("\n" + "="*60)
print(" PARTEA 2: SARCINA INDIVIDUALĂ - DIABET")
print("="*60)

print("[INFO] Se descarcă datele pentru Diabet de pe internet...")
url_diabetes = "https://raw.githubusercontent.com/susanli2016/Machine-Learning-with-Python/master/diabetes.csv"
df_diabetes = pd.read_csv(url_diabetes)

# --- SCREENSHOT 5 (Echivalentul Screenshot 1 de la Titanic) ---
print("\n[INFO] Se generează Graficul 5: Harta termică (Heatmap) pentru date lipsă...")
plt.figure(figsize=(8, 6))
sns.heatmap(df_diabetes.isnull(), yticklabels=False, cbar=False, cmap='viridis')
plt.title("SCREENSHOT 5: Diabet - Date lipsă (Zero lipsuri observate)")
plt.show()

# --- SCREENSHOT 6 (Echivalentul Screenshot 2 de la Titanic) ---
print("[INFO] Se generează Graficele 6: Distribuția cazurilor...")
plt.figure(figsize=(6, 4))
sns.set_style('whitegrid')
sns.countplot(x='Outcome', data=df_diabetes, palette='RdBu_r')
plt.title("SCREENSHOT 6.1: Diabet - Total (0=Sănătos, 1=Diabet)")
plt.show()

plt.figure(figsize=(10, 4))
sns.set_style('whitegrid')
# Folosim numărul de sarcini (Pregnancies) pe post de variabilă de grupare, cum a fost 'Sex'/'Pclass'
sns.countplot(x='Pregnancies', hue='Outcome', data=df_diabetes, palette='rainbow')
plt.title("SCREENSHOT 6.2: Diabet - Sănătos/Diabet în funcție de numărul de sarcini")
plt.show()

# --- SCREENSHOT 7 (Echivalentul Screenshot 3 de la Titanic) ---
print("[INFO] Se generează Graficele 7: Distribuția vârstelor...")
plt.figure(figsize=(8, 5))
df_diabetes['Age'].hist(bins=30, color='darkred', alpha=0.7)
plt.title("SCREENSHOT 7.1: Diabet - Histograma vârstelor")
plt.xlabel("Vârsta")
plt.show()

plt.figure(figsize=(8, 6))
# Boxplot care arată concentrația vârstelor pentru cei Sănătoși vs cu Diabet
sns.boxplot(x='Outcome', y='Age', data=df_diabetes, palette='winter')
plt.title("SCREENSHOT 7.2: Diabet - Vârsta vs Diagnostic (Boxplot)")
plt.show()


# --- PREPROCESARE ȘI ANTRENARE DIABET ---
print("\n[INFO] Se antrenează modelul pentru Diabet...")
# Setul de diabet nu necesită `get_dummies` deoarece toate datele sunt deja pur numerice
X = df_diabetes.drop('Outcome', axis=1)
y = df_diabetes['Outcome']

X_train_d, X_test_d, y_train_d, y_test_d = train_test_split(X, y, test_size=0.30, random_state=101)

logmodel_d = LogisticRegression(max_iter=1000)
logmodel_d.fit(X_train_d, y_train_d)
pred_d = logmodel_d.predict(X_test_d)

# --- SCREENSHOT 8 (DIN CONSOLĂ - Echivalentul Screenshot 4 de la Titanic) ---
print("\n" + "*"*40)
print(" 📸 FĂ SCREENSHOT LA URMĂTORUL TEXT: ")
print("*"*40)
print("\n--- SCREENSHOT 8: Rezultate Evaluare Diabet ---")
print("Matricea de confuzie:\n", confusion_matrix(y_test_d, pred_d))
print("\nRaport de clasificare:\n", classification_report(y_test_d, pred_d))
print("*"*40)
print("\n[INFO] Rularea s-a terminat cu succes!")