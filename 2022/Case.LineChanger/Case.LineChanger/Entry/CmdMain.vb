Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].ChangeLineStyles.Data

Namespace Entry

  ''' <summary>
  ''' Change and Replace Line Styles
  ''' </summary>
  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

    Implements IExternalCommand

    Public Function Execute(ByVal commandData As ExternalCommandData, ByRef message As String, ByVal elements As ElementSet) As Result Implements IExternalCommand.Execute
      Try

        ' Version
        If Not commandData.Application.Application.VersionName.Contains("2022") Then

          ' Failure
          Using td As New TaskDialog("Cannot Continue")
            With td
              .TitleAutoPrefix = False
              .MainInstruction = "Incompatible Version of Revit"
              .MainContent = "This Add-In was built, please contact CASE for assistance."
              .Show()
            End With
          End Using
          Return Result.Cancelled

        End If

        ' Construct and Display the Form
        Using m_dlg As New form_Main(New clsSettings(commandData))
          m_dlg.ShowDialog()
        End Using

        ' Success
        Return Result.Succeeded

      Catch

        ' Failure
        message = "Change and Replace Line Style Failed..."
        Return Result.Failed

      End Try

    End Function

  End Class
End Namespace