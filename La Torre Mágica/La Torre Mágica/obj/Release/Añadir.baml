   M S B A M L   `   `   `  ���� K  GLa Torre Mágica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:La_Torre_MágicaLa_Torre_Mágica      La_Torre_Mágica.Añadir   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:La_Torre_Mágica       � Title$  Añadir��$	��420��$	��450�� � WindowStartupLocation$ CenterScreen=� � Icon$ images/menu/anadir.ico�� � 
ResizeMode$ NoResize=�����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ 	LightBlue�� � Offset$	 0.0Q�� $ White��$	 1.0Q�.��� $��20q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �	 � ColumnDefinitions	 �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��Nombre:��$��Center=�$��	0,10,10,0q�$��0��$��Right=�$��30��
 �� TextAlignment$
 Right=�$
��Bold"��� -   $��TextBoxNombre��$��1��$��3��$��5q�$��30��$��Center=�$
��Left=�$��Center=� �� TextAlignment$
 Left=��� $��	Apellido:��$��Center=�$��	0,10,10,0q�$��0��$��1��$��Right=�$��30��$
 Right=�$
��Bold"��� -   $��TextBoxApellido��$��1��$��1��$��3��$��5q�$��30��$��Center=�$
��Left=�$��Center=�$
 Left=��� $��
Teléfono:��$��Center=�$��	0,10,10,0q�$��0��$��2��$��Right=�$��30��$
 Right=�$
��Bold"��� -   $��TextBoxTelefono��$��1��$��2��$��3��$��5q�$��30��$��Center=�$
��Left=�$��Center=�$
 Left=��� $��Dirección:��$��Center=�$��	0,10,10,0q�$��0��$��3��$��Right=�$��30��$
 Right=�$
��Bold"��� -   $��TextBoxDireccion��$��1��$��3��$��3��$��5q�$��30��$��Center=�$
��Left=�$��Center=�$
 Left=��� $��Código Postal:��$��Center=�$��	0,10,10,0q�$��4��$��Right=�$��30��$
 Right=�$
��Bold"��� -   $��TextBoxCodigoPostal��$��1��$��4��$��3��$��5q�$��30��$��Center=�$
��Left=�$��Center=�$
 Left=��� $��Población:��$��Center=�$��	0,10,10,0q�$��5��$��Right=�$��30��$
 Right=�$
��Bold"��� -   $��TextBoxPoblacion��$��1��$��5��$��3��$��5q�$��30��$��Center=�$
��Left=�$��Center=�$
 Left=��� $��Total Precio:��$��Center=�$��	0,10,10,0q�$��6��$��Right=�$��30��$
 Right=�$
��Bold"��� $��1��$��6�� �� Orientation$ 
Horizontal=�$��5,0,0,0q�.+��� -   $��TextBoxTotalEntero��$��55��$��30��$��Center=�$
��Left=�$��Center=�$ Center=��� $��,��$��Center=�$	��5,0q��� -   $��TextBoxTotalDecimal��$��55��$��30��$��Center=�$
��Left=�$��Center=�$ Center=��� $��Puntos:��$	��Top=�$��
0,13,117,0q�$��6��$��1��$��Right=�$��30��$
 Right=�$��2��$
��Bold"�$��3��$��62���� -	   $��TextBoxPuntos��$��3��$��6��$��47,0,5,0q�$��30�� �� 
IsReadOnly . $��Center=�$
��Left=�$��Center=�$
 Left=��� -
   $��ButtonGuardar��$��Guardar��$��7��$��4��$	��100��$��0,20,0,0q�$��35��$��Center=�{�.  