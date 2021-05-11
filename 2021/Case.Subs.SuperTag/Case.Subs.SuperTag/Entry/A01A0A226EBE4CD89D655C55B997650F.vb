Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.SuperTag.Data

Namespace Entry

  ''' <summary>
  ''' Revit 2021 Command Class 
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class A01A0A226EBE4CD89D655C55B997650F

    Implements IExternalCommand

    ''' <summary>
    ''' Command Entry Point
    ''' </summary>
    ''' <param name="commandData"></param>
    ''' <param name="message"></param>
    ''' <param name="elements"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Execute(ByVal commandData As ExternalCommandData,
                            ByRef message As String,
                            ByVal elements As ElementSet) As Result Implements IExternalCommand.Execute
      Try

        ' Version
        If Not commandData.Application.Application.VersionName.Contains("2021") Then

          Using td As New TaskDialog("Cannot Continue")
            With td
              .TitleAutoPrefix = False
              .MainInstruction = "Incompatible Revit Version"
              .MainContent = ""
              .Show()
            End With
          End Using
          Return Result.Cancelled

        End If

          ' Construct and Display the Form
          Dim m_s As New clsSettings(commandData)
          m_s.ScanCategories()
          m_s.GetSymbolNames()
          m_s.GetTags()
          m_s.GetAreasSpacesRooms()
          m_s.GetViews()

          Using d As New Form_Main(m_s)

            ' Show It
            d.ShowDialog()

          End Using

          ' Success
          Return Result.Succeeded

      Catch ex As Exception

        ' Failure Message
        message = ex.Message
        Return Result.Failed

      End Try

    End Function

  End Class
End Namespace