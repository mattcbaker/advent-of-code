Imports System

Module Program
    Sub Main(args As String())
        dim input = IO.File.ReadAllText("./input.txt").Trim()

        Console.WriteLine("first " + react(input).ToString())

        Dim lowest = Int32.MaxValue

        For i = 65 to 90
          Dim candidate = input.Replace(Convert.ToChar(i).ToString().ToLower(), "").Replace(Convert.ToChar(i).ToString(), "")

          Dim len = react(candidate)

          If len < lowest Then
            lowest = len
          End If
        Next

        Console.WriteLine("second " + lowest.ToString())
    End Sub

    Function opposites (ByVal a As char, ByVal b As Char) As Boolean
      return a <> b And a.ToString().ToUpper() = b.ToString().ToUpper()
    End Function

    Function react (ByVal input As String) As Integer
        Dim buffer As New ArrayList

        For Each c As char In input
          If buffer.Count = 0 Then
            buffer.Add(c)
          Else If Not opposites(buffer.Item(buffer.Count-1),c) Then
            buffer.Add(c)
          Else
            buffer.RemoveAt(buffer.Count-1)
          End If
        Next

        return buffer.Count
    End Function
End Module
