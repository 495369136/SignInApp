   M S B A M L   `   `   `  ����D  @SignInApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SignInApp	SignInApp  5   	   7 clr-namespace:SignInApp.ControlsSignInApp.Controls  5   	        SignInApp.WarningTipWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:SignInApp     5   	   5controls clr-namespace:SignInApp.Controls     5   	     � Title$  WarningTipWindow��5	   	   $	��160��6"   $	��600��6/    � WindowStyle$
 None=�6;    � WindowState$ Normal=�6N   .��5      � ���#E6F7FF � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $��50 �6   6,   �� 5      $��* �6   6+   �� 5      $��35 �6   6,   .+�5   
   �� $��0��6   $��1��6   $
��Auto��6.   $��5q�6<   {�. $��\Images\warning.png��6X   $��Right=�6u   �� 5   
   $��0��6   $��1��6   $��1��6.   $��1��6>   $��提示��6R   $��18(�6_   $��思源黑体 CN Normal)�6m   $��Center=�6�   $
��Left=�6�   $   SignInApp.Controls.ImageButton  5   
   $   SignInApp.Controls.ImageButton   Click$ CloseClickHandler��6�   $��0��6   $��1��6,   $��2��6=   $��1��6M   $
��Auto��6a   $��
5 10 15 10q�6o   
������ {�.    	ImagePath$ \Images\close.png��6�      CornerRadius$ 0��6�   6�   �� 5   
   $��1��6   $��3��6   $��1��6.   $��1��6>   $��16(�6R   ���#333333$��思源黑体 CN Light)�6u   $
��Left=�6�   $	��Top=�6�   .��5      �� -   5         ��Name$ 	tip_value��5      $
��Wrap=�6+   5      5      5      