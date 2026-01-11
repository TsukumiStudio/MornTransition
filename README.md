# MornTransition

画面遷移エフェクトを管理するライブラリ

## 依存関係

- UniTask
- MornGlobal
- MornEnum

## セットアップ

1. `Project`を右クリック → `Morn/MornTransitionGlobal`を作成
2. `TransitionNames`にトランジション名を設定
3. シーンに`MornTransitionCore`を配置
4. 子オブジェクトに`MornTransitionBase`継承コンポーネントを追加

## 使い方

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
// トランジション付きでシーン遷移
await MornTransitionCore.FillAsync(transitionType, ct);
await SceneManager.LoadSceneAsync("NextScene");
await MornTransitionCore.ClearAsync(ct);
```

## 主要クラス

| クラス | 機能 |
|---|---|
| `MornTransitionCore` | トランジションのシングルトン管理 |
| `MornTransitionBase` | トランジション実装の基底クラス |
| `MornTransitionType` | トランジション種類の識別子 |
| `MornTransitionGlobal` | グローバル設定（トランジション名リスト） |

## トランジション種類

| コンポーネント | 説明 |
|---|---|
| `MornTransitionFade` | Imageのアルファ値でフェード |
| `MornTransitionAnimation` | Animatorでアニメーション再生 |
| `MornTransitionMaterial` | シェーダーパラメータでトランジション |

## カスタムトランジション

`MornTransitionBase`を継承して独自のトランジションを実装可能

```csharp
public class MyTransition : MornTransitionBase
{
    public override async UniTask FillAsync(CancellationToken ct = default)
    {
        // 画面を覆う処理
    }

    public override async UniTask ClearAsync(CancellationToken ct = default)
    {
        // 画面を表示する処理
    }

    public override void FillImmediate()
    {
        // 即時に覆う
    }

    public override void ClearImmediate()
    {
        // 即時に表示
    }
}
```
