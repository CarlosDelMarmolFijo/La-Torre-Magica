   M S B A M L   `   `   `  ����  La Torre Mágica(    "La_Torre_Mágica.ActualizarCliente   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$"  Actualizar Gasto del Cliente��5   	   $	��200��5   	   $	��400��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	    � Icon$ images/menu/modificar.png��5	   	   ��5      �� 5   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 1,1&�6/   .��5      �  � Color$ 	LightBlue��6    � Offset$	 0.0Q�6-   � 5      $ White��6   $	 1.0Q�6)   5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   	 � ColumnDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5!   
   �� $��Nombre del cliente:��6   $��Center=�6/   $��0,0,10,0q�6J   $��0��6\   $��0��6l   $��Right=�6y   $
��Bold"�6�   �� 5"   
   -   5        $��TextBoxNombreCliente��5"      $��1��6.   $��0��6>   $��5q�6K   $��30��6V   $��Center=�6b   $
��Left=�6}   $��Center=�6�   
 �� TextAlignment$

 Left=�6�   �� 5%   
   $��Cantidad gastada:��6   $��Center=�6-   $��0,0,10,0q�6H   $��0��6Z   $��1��6j   $��Right=�6w   $
��Bold"�6�   �� 5&   
   -   5        $��TextBoxCantidadGastada��5&      $��1��60   $��1��6@   $��5q�6M   $��30��6X   $��Center=�6d   $
��Left=�6   $��Center=�6�   $

 Left=�6�   �� 5)   
   -   5        $��ButtonAceptar��5)      $��
Actualizar��5*      $��1��5+      $��2��5,      $	��100��5-      $��5q�5.      $��30��5/      $��Right=�50      ���#2196F3
����������#2196F3$��1q�55      56      57      