# ============================================================
# Lucrarea practică nr. 6 – CNN pentru clasificarea diabetului
# Dataset: Pima Indians Diabetes (Kaggle / UCI)
# ============================================================

# ─── Pasul 1. Importul bibliotecilor ────────────────────────
import numpy as np
import pandas as pd
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
from sklearn.metrics import (classification_report, confusion_matrix,
                             roc_curve, auc, ConfusionMatrixDisplay)
import tensorflow as tf
from tensorflow.keras.models import Sequential, Model
from tensorflow.keras.layers import (Dense, Dropout, BatchNormalization,
                                     Conv1D, MaxPooling1D, Flatten,
                                     GlobalMaxPooling1D, Activation,
                                     Input, GlobalAveragePooling1D,
                                     Add)
from tensorflow.keras.callbacks import EarlyStopping, ReduceLROnPlateau
from tensorflow.keras.applications import ResNet50
import warnings
warnings.filterwarnings('ignore')

tf.random.set_seed(42)
np.random.seed(42)

# ─── Pasul 2. Citirea setului de date ───────────────────────
url = ("https://raw.githubusercontent.com/plotly/datasets/master/"
       "diabetes.csv")
try:
    df = pd.read_csv(url)
    print("Date descărcate cu succes din web.")
except Exception:
    # fallback: creare date sintetice cu distribuție realistă
    print("Generare date sintetice (fără acces internet).")
    np.random.seed(42)
    n = 768
    df = pd.DataFrame({
        'Pregnancies':      np.random.poisson(3.8, n),
        'Glucose':          np.clip(np.random.normal(120, 32, n), 44, 199).astype(int),
        'BloodPressure':    np.clip(np.random.normal(69, 19, n), 24, 122).astype(int),
        'SkinThickness':    np.clip(np.random.normal(20, 16, n), 0, 99).astype(int),
        'Insulin':          np.clip(np.random.exponential(79, n), 0, 846).astype(int),
        'BMI':              np.clip(np.random.normal(32, 7, n), 0, 67).round(1),
        'DiabetesPedigreeFunction': np.clip(np.random.exponential(0.47, n), 0.08, 2.42).round(3),
        'Age':              np.clip(np.random.normal(33, 12, n), 21, 81).astype(int),
    })
    # etichetă sintetică corelată cu glucoza
    df['Outcome'] = ((df['Glucose'] > 127) |
                     ((df['BMI'] > 30) & (df['Age'] > 40))).astype(int)

print("\n── Primele 5 rânduri ──")
print(df.head())
print("\n── Informații generale ──")
print(df.info())
print("\n── Statistici descriptive ──")
print(df.describe().round(2))

# ─── Pasul 3. Analiza explorativă (EDA) ─────────────────────
# 3a. Distribuția claselor
fig, axes = plt.subplots(1, 2, figsize=(12, 5))
counts = df['Outcome'].value_counts()
axes[0].bar(['Non-diabetic (0)', 'Diabetic (1)'], counts.values,
            color=['#2196F3', '#F44336'], edgecolor='black')
axes[0].set_title('Distribuția claselor (Outcome)', fontsize=13, fontweight='bold')
axes[0].set_ylabel('Număr de exemple')
for i, v in enumerate(counts.values):
    axes[0].text(i, v + 5, str(v), ha='center', fontsize=12, fontweight='bold')

# 3b. Histograme toate caracteristicile
df.hist(bins=25, figsize=(14, 10), color='steelblue', edgecolor='black')
plt.suptitle('Distribuția caracteristicilor', fontsize=14, fontweight='bold')
plt.tight_layout()
plt.savefig('/home/claude/fig1_distributie_clase.png', dpi=150, bbox_inches='tight')
plt.close()
print("fig1 salvat")

# 3c. Matricea de corelație
plt.figure(figsize=(10, 8))
corr = df.corr()
mask = np.triu(np.ones_like(corr, dtype=bool))
sns.heatmap(corr, mask=mask, annot=True, fmt='.2f', cmap='coolwarm',
            square=True, linewidths=0.5, cbar_kws={'shrink': 0.8})
plt.title('Matricea de corelație', fontsize=13, fontweight='bold')
plt.tight_layout()
plt.savefig('/home/claude/fig2_corelatie.png', dpi=150, bbox_inches='tight')
plt.close()
print("fig2 salvat")

# 3d. Boxplots caracteristici vs Outcome
feature_cols = [c for c in df.columns if c != 'Outcome']
fig, axes = plt.subplots(2, 4, figsize=(16, 8))
axes = axes.flatten()
for i, col in enumerate(feature_cols):
    df.boxplot(column=col, by='Outcome', ax=axes[i], patch_artist=True)
    axes[i].set_title(col, fontsize=10)
    axes[i].set_xlabel('Outcome')
plt.suptitle('Distribuția caracteristicilor pe clase', fontsize=13, fontweight='bold')
plt.tight_layout()
plt.savefig('/home/claude/fig3_boxplots.png', dpi=150, bbox_inches='tight')
plt.close()
print("fig3 salvat")

# ─── Pasul 4. Preprocesarea datelor ─────────────────────────
# Înlocuirea valorilor zero (imposibile biologic) cu mediana
zero_cols = ['Glucose', 'BloodPressure', 'SkinThickness', 'Insulin', 'BMI']
for col in zero_cols:
    median_val = df[col][df[col] != 0].median()
    df[col] = df[col].replace(0, median_val)

X = df[feature_cols].values
y = df['Outcome'].values

scaler = StandardScaler()
X_scaled = scaler.fit_transform(X)

# ─── Pasul 5. Împărțirea setului de date ────────────────────
X_train, X_test, y_train, y_test = train_test_split(
    X_scaled, y, test_size=0.2, random_state=42, stratify=y)
X_train, X_val, y_train, y_val = train_test_split(
    X_train, y_train, test_size=0.15, random_state=42, stratify=y_train)

print(f"\nAntrenament: {X_train.shape[0]} | Validare: {X_val.shape[0]} | Test: {X_test.shape[0]}")

# Reshape pentru Conv1D: (samples, steps, features) → (samples, features, 1)
X_train_c = X_train.reshape(X_train.shape[0], X_train.shape[1], 1)
X_val_c   = X_val.reshape(X_val.shape[0],   X_val.shape[1],   1)
X_test_c  = X_test.reshape(X_test.shape[0],  X_test.shape[1],  1)

# ─── Pasul 6. Construirea modelului CNN ─────────────────────
def build_cnn(input_shape):
    model = Sequential([
        # Bloc 1
        Conv1D(filters=32, kernel_size=3, padding='same', input_shape=input_shape),
        Activation('relu'),
        BatchNormalization(),
        MaxPooling1D(pool_size=2),
        Dropout(0.2),
        # Bloc 2
        Conv1D(filters=64, kernel_size=3, padding='same'),
        Activation('relu'),
        BatchNormalization(),
        MaxPooling1D(pool_size=2),
        Dropout(0.2),
        # Bloc 3
        Conv1D(filters=128, kernel_size=3, padding='same'),
        Activation('relu'),
        BatchNormalization(),
        GlobalMaxPooling1D(),
        Dropout(0.3),
        # Straturi Dense
        Dense(128),
        Activation('relu'),
        Dropout(0.3),
        Dense(64),
        Activation('relu'),
        Dropout(0.2),
        Dense(1),
        Activation('sigmoid'),
    ], name='CNN_Diabet')
    return model

# ─── Pasul 7. Rezumat model CNN ─────────────────────────────
cnn_model = build_cnn((X_train_c.shape[1], 1))
cnn_model.summary()

cnn_model.compile(optimizer='adam',
                  loss='binary_crossentropy',
                  metrics=['accuracy',
                           tf.keras.metrics.AUC(name='auc'),
                           tf.keras.metrics.Precision(name='precision'),
                           tf.keras.metrics.Recall(name='recall')])

callbacks = [
    EarlyStopping(monitor='val_auc', patience=15, restore_best_weights=True, mode='max'),
    ReduceLROnPlateau(monitor='val_loss', factor=0.5, patience=7, verbose=0)
]

# ─── Pasul 8. Antrenarea CNN ─────────────────────────────────
history_cnn = cnn_model.fit(
    X_train_c, y_train,
    validation_data=(X_val_c, y_val),
    epochs=100, batch_size=32,
    callbacks=callbacks, verbose=1
)

# ─── Pasul 9. Curbe de antrenament CNN ───────────────────────
fig, axes = plt.subplots(1, 3, figsize=(16, 5))
metrics_to_plot = [('accuracy', 'Acuratețe'), ('loss', 'Pierdere (Loss)'), ('auc', 'AUC')]
colors = [('#1976D2', '#F57C00'), ('#388E3C', '#D32F2F'), ('#7B1FA2', '#F57C00')]
for ax, (metric, label), (c1, c2) in zip(axes, metrics_to_plot, colors):
    ax.plot(history_cnn.history[metric],        color=c1, linewidth=2, label='Antrenament')
    ax.plot(history_cnn.history[f'val_{metric}'],color=c2, linewidth=2, label='Validare')
    ax.set_title(f'CNN – {label}', fontsize=12, fontweight='bold')
    ax.set_xlabel('Epocă'); ax.set_ylabel(label)
    ax.legend(); ax.grid(True, alpha=0.3)
plt.suptitle('Curbe de antrenament – CNN', fontsize=14, fontweight='bold')
plt.tight_layout()
plt.savefig('/home/claude/fig4_cnn_training.png', dpi=150, bbox_inches='tight')
plt.close()
print("fig4 salvat")

# ─── Pasul 10. Evaluare CNN ──────────────────────────────────
cnn_results = cnn_model.evaluate(X_test_c, y_test, verbose=0)
print("\n── Evaluare CNN pe setul de test ──")
for name, val in zip(cnn_model.metrics_names, cnn_results):
    print(f"  {name}: {val:.4f}")

y_pred_cnn  = (cnn_model.predict(X_test_c) > 0.5).astype(int).flatten()
y_prob_cnn  = cnn_model.predict(X_test_c).flatten()
print("\n── Raport clasificare CNN ──")
print(classification_report(y_test, y_pred_cnn, target_names=['Non-diab.','Diabetic']))

# ─── Pasul 11. Modele alternative: MLP și ResNet-style ──────
# Model MLP (baseline)
def build_mlp(input_dim):
    model = Sequential([
        Dense(256, input_dim=input_dim), Activation('relu'), BatchNormalization(), Dropout(0.3),
        Dense(128), Activation('relu'), BatchNormalization(), Dropout(0.3),
        Dense(64),  Activation('relu'), Dropout(0.2),
        Dense(1),   Activation('sigmoid'),
    ], name='MLP_Baseline')
    return model

mlp_model = build_mlp(X_train.shape[1])
mlp_model.compile(optimizer='adam', loss='binary_crossentropy',
                  metrics=['accuracy', tf.keras.metrics.AUC(name='auc')])
history_mlp = mlp_model.fit(X_train, y_train,
    validation_data=(X_val, y_val),
    epochs=100, batch_size=32, callbacks=callbacks, verbose=0)

# ResNet-style cu skip connections
def build_resnet_style(input_shape):
    inp = Input(shape=input_shape)
    x = Conv1D(64, 3, padding='same', activation='relu')(inp)
    x = BatchNormalization()(x)
    skip = x
    x = Conv1D(64, 3, padding='same', activation='relu')(x)
    x = BatchNormalization()(x)
    x = Add()([x, skip])   # skip connection
    x = Conv1D(128, 3, padding='same', activation='relu')(x)
    x = BatchNormalization()(x)
    x = GlobalAveragePooling1D()(x)
    x = Dense(64, activation='relu')(x)
    x = Dropout(0.3)(x)
    out = Dense(1, activation='sigmoid')(x)
    model = Model(inp, out, name='ResNet_Style')
    return model

resnet_model = build_resnet_style((X_train_c.shape[1], 1))
resnet_model.compile(optimizer='adam', loss='binary_crossentropy',
                     metrics=['accuracy', tf.keras.metrics.AUC(name='auc')])
history_res = resnet_model.fit(X_train_c, y_train,
    validation_data=(X_val_c, y_val),
    epochs=100, batch_size=32, callbacks=callbacks, verbose=0)

# ─── Comparare modele ────────────────────────────────────────
mlp_res    = mlp_model.evaluate(X_test,   y_test, verbose=0)
resnet_res = resnet_model.evaluate(X_test_c, y_test, verbose=0)

y_pred_mlp    = (mlp_model.predict(X_test)    > 0.5).astype(int).flatten()
y_prob_mlp    = mlp_model.predict(X_test).flatten()
y_pred_res    = (resnet_model.predict(X_test_c) > 0.5).astype(int).flatten()
y_prob_res    = resnet_model.predict(X_test_c).flatten()

models_names  = ['CNN', 'MLP', 'ResNet-style']
accs  = [cnn_results[1],   mlp_res[1],   resnet_res[1]]
aucs  = [cnn_results[2],   mlp_res[2],   resnet_res[2]]

print("\n── Comparare modele ──")
for name, acc, a in zip(models_names, accs, aucs):
    print(f"  {name:<14} Acc={acc:.4f}  AUC={a:.4f}")

# ─── Diagrama comparativă ────────────────────────────────────
fig, axes = plt.subplots(1, 2, figsize=(12, 5))
x = np.arange(len(models_names)); w = 0.4
axes[0].bar(x, accs, width=w, color=['#1976D2','#388E3C','#F57C00'], edgecolor='black')
axes[0].set_xticks(x); axes[0].set_xticklabels(models_names)
axes[0].set_ylim(0.5, 1.0); axes[0].set_title('Acuratețe test', fontweight='bold')
axes[0].set_ylabel('Accuracy')
for i, v in enumerate(accs): axes[0].text(i, v+0.005, f'{v:.3f}', ha='center', fontweight='bold')

axes[1].bar(x, aucs, width=w, color=['#1976D2','#388E3C','#F57C00'], edgecolor='black')
axes[1].set_xticks(x); axes[1].set_xticklabels(models_names)
axes[1].set_ylim(0.5, 1.0); axes[1].set_title('AUC test', fontweight='bold')
axes[1].set_ylabel('AUC')
for i, v in enumerate(aucs): axes[1].text(i, v+0.005, f'{v:.3f}', ha='center', fontweight='bold')

plt.suptitle('Comparare modele – Acuratețe și AUC', fontsize=13, fontweight='bold')
plt.tight_layout()
plt.savefig('/home/claude/fig5_comparare_modele.png', dpi=150, bbox_inches='tight')
plt.close()
print("fig5 salvat")

# ─── Matrici de confuzie ─────────────────────────────────────
fig, axes = plt.subplots(1, 3, figsize=(15, 4))
for ax, y_p, title in zip(axes,
        [y_pred_cnn, y_pred_mlp, y_pred_res],
        ['CNN', 'MLP', 'ResNet-style']):
    cm = confusion_matrix(y_test, y_p)
    disp = ConfusionMatrixDisplay(cm, display_labels=['Non-diab.','Diabetic'])
    disp.plot(ax=ax, colorbar=False, cmap='Blues')
    ax.set_title(f'Matrice confuzie – {title}', fontweight='bold')
plt.tight_layout()
plt.savefig('/home/claude/fig6_confusion_matrices.png', dpi=150, bbox_inches='tight')
plt.close()
print("fig6 salvat")

# ─── Curbe ROC ───────────────────────────────────────────────
plt.figure(figsize=(8, 6))
for y_p, name, color in zip([y_prob_cnn, y_prob_mlp, y_prob_res],
                              models_names, ['#1976D2','#388E3C','#F57C00']):
    fpr, tpr, _ = roc_curve(y_test, y_p)
    roc_auc = auc(fpr, tpr)
    plt.plot(fpr, tpr, color=color, linewidth=2, label=f'{name} (AUC={roc_auc:.3f})')
plt.plot([0,1],[0,1],'k--', linewidth=1)
plt.xlabel('False Positive Rate'); plt.ylabel('True Positive Rate')
plt.title('Curbe ROC – Comparare modele', fontsize=13, fontweight='bold')
plt.legend(loc='lower right'); plt.grid(True, alpha=0.3)
plt.tight_layout()
plt.savefig('/home/claude/fig7_roc_curves.png', dpi=150, bbox_inches='tight')
plt.close()
print("fig7 salvat")

# ─── Importanța caracteristicilor (permutation) ──────────────
from sklearn.inspection import permutation_importance
from sklearn.base import BaseEstimator, ClassifierMixin

class KerasWrapper(BaseEstimator, ClassifierMixin):
    def __init__(self, model, reshape=False):
        self.model = model; self.reshape = reshape
    def fit(self, X, y): return self
    def predict(self, X):
        inp = X.reshape(X.shape[0], X.shape[1], 1) if self.reshape else X
        return (self.model.predict(inp, verbose=0) > 0.5).astype(int).flatten()
    def score(self, X, y):
        return np.mean(self.predict(X) == y)

wrapper = KerasWrapper(cnn_model, reshape=True)
perm = permutation_importance(wrapper, X_test, y_test, n_repeats=10, random_state=42)
feat_imp = pd.Series(perm.importances_mean, index=feature_cols).sort_values(ascending=True)

plt.figure(figsize=(8, 5))
feat_imp.plot(kind='barh', color='steelblue', edgecolor='black')
plt.title('Importanța caracteristicilor (Permutation – CNN)', fontweight='bold')
plt.xlabel('Reducerea medie a acurateței')
plt.tight_layout()
plt.savefig('/home/claude/fig8_feature_importance.png', dpi=150, bbox_inches='tight')
plt.close()
print("fig8 salvat")

print("\n=== Toate figurile au fost generate cu succes! ===")

# ─── Salvare rezultate numerice pentru raport ─────────────────
results_summary = {
    'CNN':         {'acc': cnn_results[1],   'auc': cnn_results[2]},
    'MLP':         {'acc': mlp_res[1],       'auc': mlp_res[2]},
    'ResNet-style':{'acc': resnet_res[1],    'auc': resnet_res[2]},
}
for m, v in results_summary.items():
    print(f"{m}: Acc={v['acc']:.4f}, AUC={v['auc']:.4f}")