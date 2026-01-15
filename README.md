# MornTransition

## 概要

画面遷移エフェクト（フェードなど）を管理し、非同期で実行・制御するライブラリ。

## 依存関係

| 種別 | 名前 |
|------|------|
| 外部パッケージ | UniTask |
| Mornライブラリ | MornGlobal, MornEnum |

## 使い方

### セットアップ

1. Projectウィンドウで右クリック → `Morn/MornTransitionGlobal` を作成
2. `TransitionNames` にトランジション名を設定
3. シーンに `MornTransitionCore` を配置
4. 子オブジェクトに `MornTransitionBase` 継承コンポーネントを追加

### 基本操作

```csharp
// 画面を覆う（フェードイン）
await MornTransitionCore.FillAsync(transitionType, ct);

// 画面を表示（フェードアウト）
await MornTransitionCore.ClearAsync(ct);

// 即時実行版
MornTransitionCore.FillImmediate(transitionType);
MornTransitionCore.ClearImmediate();

// 現在覆われているか
bool isFilling = MornTransitionCore.IsFilling();
```

### シーン遷移の例

```csharp
await MornTransitionCore.FillAsync(transitionType, ct);
await SceneManager.LoadSceneAsync("NextScene");
await MornTransitionCore.ClearAsync(ct);
```

### トランジション種類

| コンポーネント | 説明 |
|---------------|------|
| MornTransitionFade | Imageのアルファ値でフェード |
| MornTransitionAnimation | Animatorでアニメーション再生 |
| MornTransitionMaterial | シェーダーパラメータでトランジション |
