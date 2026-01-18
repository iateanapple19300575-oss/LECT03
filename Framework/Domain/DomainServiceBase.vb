Imports Framework.Core
Imports Framework.Data
Imports Framework.Validation

Namespace Framework.Domain

    ''' <summary>
    ''' ビジネスロジック層（ドメインサービス）の基底クラス。
    ''' トランザクション管理、バリデーション、例外処理を統一し、
    ''' 各サービスクラスが共通の実行フローを持つようにする。
    ''' </summary>
    Public MustInherit Class DomainServiceBase

        ''' <summary>
        ''' データベース接続文字列。
        ''' </summary>
        Protected ReadOnly _connectionString As String

        ''' <summary>
        ''' 接続文字列を指定してドメインサービスを初期化する。
        ''' </summary>
        ''' <param name="connectionString">DB 接続文字列。</param>
        Protected Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        ' ============================
        ' Template Method：業務処理の流れを統一
        ' ============================

        ''' <summary>
        ''' トランザクションを伴う業務処理を実行する。
        ''' <para>
        ''' ・トランザクション開始  
        ''' ・業務処理（action）実行  
        ''' ・コミット  
        ''' ・例外時はロールバック  
        ''' </para>
        ''' </summary>
        ''' <typeparam name="T">処理結果の型。</typeparam>
        ''' <param name="action">業務処理本体。<see cref="SqlExecutor"/> を受け取り結果を返す。</param>
        ''' <returns>業務処理の戻り値。</returns>
        Protected Function Execute(Of T)(action As Func(Of SqlExecutor, T)) As T
            Try
                Using exec As New SqlExecutor(_connectionString)
                    exec.BeginTransaction()

                    Dim result As T = action(exec)

                    exec.Commit()
                    Return result
                End Using

            Catch ex As ValidationException
                ' バリデーション例外はロールバックしてそのまま UI に返す
                Throw

            Catch ex As Exception
                ' 予期しない例外は ErrorHandler に統合
                Throw New Exception(ErrorHandler.Handle(ex, "DomainService.Execute"))
            End Try
        End Function

        ''' <summary>
        ''' 戻り値を持たない業務処理を実行する。
        ''' </summary>
        ''' <param name="action">業務処理本体。</param>
        Protected Sub Execute(action As Action(Of SqlExecutor))
            Execute(Of Object)(
                Function(exec)
                    action(exec)
                    Return Nothing
                End Function
            )
        End Sub

        ' ============================
        ' Validation Helper
        ' ============================

        ''' <summary>
        ''' 指定されたエンティティをバリデーションする。
        ''' </summary>
        ''' <typeparam name="TEntity">バリデーション対象のエンティティ型。</typeparam>
        ''' <param name="entity">バリデーション対象のエンティティ。</param>
        ''' <param name="validator">バリデータ。</param>
        ''' <exception cref="ValidationException">バリデーションエラーがある場合にスローされる。</exception>
        Protected Sub Validate(Of TEntity)(entity As TEntity, validator As IValidator(Of TEntity))
            Dim result = validator.Validate(entity)
            If Not result.IsValid Then
                Throw New ValidationException(result.Errors)
            End If
        End Sub

    End Class

End Namespace