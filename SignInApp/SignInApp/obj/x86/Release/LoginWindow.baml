   M S B A M L   `   `   `  ���� D  @SignInApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SignInApp	SignInApp      SignInApp.LoginWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:SignInApp       � Title$  登录��$	��360��$	��560�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�
������  � AllowsTransparency . .���� ���#F5F5F5$��0 0 0 0q�$	��360��$��Center=�$	��560�� �� CornerRadius$ 10��
�������._��  � ColumnDefinitions �� $	��230 ��� $��56 ��� $��* ��� $��56 �.+��� -    ��Name$ broder_back��$y�	Collapsed=�$ 10��$��0��$��4��
������ $	��Top=� K� ZIndex$ 2��._�   SignInApp.LoadingWait  -   	  Name$	 _loading��$y�	Collapsed=�� $��0��$��1��.+�� 
 � RowDefinitions
 �� �� �� �� �� �� �� �� �� �� �� .+��� $��0��$��11��$ 0��._��� $&�� /Images/loginBackgroundImage.png��$	��top=�$	��360��$	��300���� $��0��$��1��$ 1��$��30,25,0,-25q�$y�	Collapsed=�._��� -    ��Name$ competent_organization��$��12(����#FFFFFF$ ��思源黑体 CN ExtraLight)��� $��2��$��1��$ 1��$��30,16,0,-25q�._��� -   $ province��$��24(����#FFFFFF$��思源黑体 CN Medium)��� $��3��$��1��$ 1��$��30,18,0,-25q�._��� -   $ app_type��$��24(����#FFFFFF$��思源黑体 CN Medium)��� $��4��$��1��$ 1��$��30,20,0,-30q�._��� -   $ app_name��$��24(����#FFFFFF$��思源黑体 CN Medium)��� $��7��$��1��$ 1��$��30,32,0,-30q�._��� $��技术支持:��$��12(����#FFFFFF$ ��思源黑体 CN ExtraLight)��� $��8��$��1��$ 1��$��30,16,0,-10q�._��� -   $ support_company��$��12(����#FFFFFF$ ��思源黑体 CN ExtraLight)��� $��9��$��1��$ 1��$��30,0,0,6q�._��� -   $ support_tel��$��12(����#FFFFFF$ ��思源黑体 CN ExtraLight)�� $��2��$��1��
 �� �� �� �� �� �� �� �� �� �� �� .+��� $��1��$��1��$ 1��$��0q�$��0,0,0,2q����#43A047$��Center=�._��� $��登陆��$��17(����#333333$��思源黑体 CN Medium)��� $��4��$��1��$ 1��$��
0,-19,0,19q�$��0,0,0,1q����#666666._�� .+��� $��/Images/account.png��$��18��$��20��$��9,7,8,0q��� -	    ��Name$ account��$��16(�$��思源黑体 CN Medium)�$	��170��$��0,8,0,0q�$��0q����#333333.�� � 	Resources    	HelpBrush j� TileMode �� Opacity j� Stretch j� 
AlignmentX !� Visual��%�   &	        !� $
 None=�$	 0.3Q�$
 None=�$
 Left=� �� $��思源黑体 CN Medium)�$��14(����#C0C0C0$��账号��.������  �� 
TargetType" �� �� Triggers P�  P� Property � �� P� Value Z� . ���  �� Property � �� �� Value# [  P�  � ��$  ��. ���  � ��# [  .���� $��6��$��1��$��5��$��4��$ 1��$��
0,-23,0,23q�$��0,0,0,1q����#666666._�� .+��� $��/Images/password.png��$��18��$��20��$��9,7,8,0q�G� -
    G�Name$ password��$��16(�$��思源黑体 CN Medium)�$	��170��$��0,8,0,0q�$��0q����#333333�� $��7��$��1��$��6��$��2��$ 1��$��
0,-15,0,15q����#666666._��� -    ��Name$ 
remainFlag��$��记住密码��$��Center=�$��Center=�$��13(����#666666�� $��8��$��1��$��5��$��4��$ 1��$��	0,-10,0,8q�._��� -   $��登录��$��0q� � Cursor$
 Hand��$��14(�
�������$��思源黑体 CN Medium)�����  �� 
TargetType" ��.R��� #��z��$��0q�$ 16�����#43A047._��� #��z��$��Center=�$��Center=��� $��9��$��1��$��7��$��1��$ 1��$��	0, 0,0,12q�$��0,0,0,0q����#43A047$��Center=�._��� -   $
 Hand��
�������$��0q�.���� $��退出��$��14(�$��思源黑体 CN Medium)����#43A047$��	Underline{�