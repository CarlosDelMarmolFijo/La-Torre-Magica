   M S B A M L   `   `   `  ����K  GLa Torre Mágica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:La_Torre_MágicaLa_Torre_Mágica  5   	       La_Torre_Mágica.Añadir   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:La_Torre_Mágica     5   	     � Title$  Añadir��5   	   $	��420��5	   	   $	��450��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ images/menu/anadir.ico��5   	    � 
ResizeMode$ NoResize=�5   	   ��5      �� 5   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 1,1&�6/   .��5      �  � Color$ 	LightBlue��6    � Offset$	 0.0Q�6-   � 5      $ White��6   $	 1.0Q�6)   5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   	 � ColumnDefinitions	 5$   
   �� 5%      $
��Auto �6   �� 5&      $��* �6   �� 5'      $
��Auto �6   �� 5(      $��* �6   .+�5,   
   �� $��Nombre:��6   $��Center=�6#   $��	0,10,10,0q�6>   $��0��6Q   $��Right=�6a   $��30��6}   
 �� TextAlignment$
 Right=�6�   $
��Bold"�6�   �� 5-   
   -   5        $��TextBoxNombre��5-      $��1��6'   $��3��67   $��5q�6K   $��30��6V   $��Center=�6b   $
��Left=�6}   $��Center=�6�    �� TextAlignment$
 Left=�6�   6�   �� 50   
   $��	Apellido:��6   $��Center=�6%   $��	0,10,10,0q�6@   $��0��6S   $��1��6c   $��Right=�6p   $��30��6�   $
 Right=�6�   $
��Bold"�6�   �� 51   
   -   5        $��TextBoxApellido��51      $��1��6)   $��1��69   $��3��6F   $��5q�6Z   $��30��6e   $��Center=�6q   $
��Left=�6�   $��Center=�6�   $
 Left=�6�   6�   �� 54   
   $��
Teléfono:��6   $��Center=�6%   $��	0,10,10,0q�6@   $��0��6S   $��2��6c   $��Right=�6p   $��30��6�   $
 Right=�6�   $
��Bold"�6�   �� 55   
   -   5        $��TextBoxTelefono��55      $��1��6)   $��2��69   $��3��6F   $��5q�6Z   $��30��6e   $��Center=�6q   $
��Left=�6�   $��Center=�6�   $
 Left=�6�   6�   �� 58   
   $��Dirección:��6   $��Center=�6&   $��	0,10,10,0q�6A   $��0��6T   $��3��6d   $��Right=�6q   $��30��6�   $
 Right=�6�   $
��Bold"�6�   �� 59   
   -   5        $��TextBoxDireccion��59      $��1��6*   $��3��6:   $��3��6G   $��5q�6[   $��30��6f   $��Center=�6r   $
��Left=�6�   $��Center=�6�   $
 Left=�6�   6�   �� 5<   
   $��Código Postal:��6   $��Center=�6*   $��	0,10,10,0q�6E   $��4��6X   $��Right=�6e   $��30��6�   $
 Right=�6�   $
��Bold"�6�   �� 5=   
   -   5        $��TextBoxCodigoPostal��5=      $��1��6-   $��4��6=   $��3��6J   $��5q�6^   $��30��6i   $��Center=�6u   $
��Left=�6�   $��Center=�6�   $
 Left=�6�   6�   �� 5@   
   $��Población:��6   $��Center=�6&   $��	0,10,10,0q�6A   $��5��6T   $��Right=�6a   $��30��6}   $
 Right=�6�   $
��Bold"�6�   �� 5A   
   -   5        $��TextBoxPoblacion��5A      $��1��6*   $��5��6:   $��3��6G   $��5q�6[   $��30��6f   $��Center=�6r   $
��Left=�6�   $��Center=�6�   $
 Left=�6�   6�   �� 5D   
   $��Total Precio:��6   $��Center=�6)   $��	0,10,10,0q�6D   $��6��6W   $��Right=�6d   $��30��6�   $
 Right=�6�   $
��Bold"�6�   �� 5E   
   $��1��6   $��6��6%    �� Orientation$ 
Horizontal=�62   $��5,0,0,0q�6K   .+�5F      �� -   5        $��TextBoxTotalEntero��5F      $��55��60   $��30��6;   $��Center=�6G   $
��Left=�6b   $��Center=�6�   $ Center=�6�   6�   �� 5G      $��,��6   $��Center=�6!   $	��5,0q�6<   �� 5H      -   5        $��TextBoxTotalDecimal��5H      $��55��61   $��30��6<   $��Center=�6H   $
��Left=�6c   $��Center=�6�   $ Center=�6�   6�   5I      �� 5L   
   $��Puntos:��6   $	��Top=�6#   $��
0,13,117,0q�6;   $��6��6O   $��1��6\   $��Right=�6l   $��30��6�   $
 Right=�6�   $��2��6�   $
��Bold"�6�   $��3��6�   $��62��6�   �� 5M   
   -	   5        $��TextBoxPuntos��5M      $��3��6'   $��6��67   $��47,0,5,0q�6D   $��30��6V    �� 
IsReadOnly . $��Center=�6t   $
��Left=�6�   $��Center=�6�   $
 Left=�6�   �� 5P   
   -
   5        $��ButtonGuardar��5P      $��Guardar��6&   $��7��68   $��4��6E   $	��100��6Y   $��0,20,0,0q�6e   $��35��6w   $��Center=�6�   {�.  6�   5Q      5R      