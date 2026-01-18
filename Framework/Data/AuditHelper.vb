Namespace Framework.Data

    ''' <summary>
    ''' 監査情報（作成日時・更新日時）を自動設定するためのヘルパークラス。
    ''' エンティティが <see cref="IAuditable"/> を実装している場合のみ処理を行う。
    ''' </summary>
    Public NotInheritable Class AuditHelper

        ''' <summary>
        ''' 新規作成時の監査情報を設定する。
        ''' <para>
        ''' ・CreatedAt と UpdatedAt の両方に現在日時を設定する。  
        ''' ・エンティティが <see cref="IAuditable"/> を実装していない場合は何もしない。
        ''' </para>
        ''' </summary>
        ''' <param name="entity">監査情報を設定する対象のエンティティ。</param>
        Public Shared Sub SetForInsert(entity As Object)
            If TypeOf entity Is IAuditable Then
                Dim aud = DirectCast(entity, IAuditable)
                Dim now = DateTime.Now
                aud.CreatedAt = now
                aud.UpdatedAt = now
            End If
        End Sub

        ''' <summary>
        ''' 更新時の監査情報を設定する。
        ''' <para>
        ''' ・UpdatedAt のみ現在日時に更新する。  
        ''' ・エンティティが <see cref="IAuditable"/> を実装していない場合は何もしない。
        ''' </para>
        ''' </summary>
        ''' <param name="entity">監査情報を設定する対象のエンティティ。</param>
        Public Shared Sub SetForUpdate(entity As Object)
            If TypeOf entity Is IAuditable Then
                Dim aud = DirectCast(entity, IAuditable)
                aud.UpdatedAt = DateTime.Now
            End If
        End Sub

    End Class

End Namespace