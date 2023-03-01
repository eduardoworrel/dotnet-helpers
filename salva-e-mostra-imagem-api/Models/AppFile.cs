using System.ComponentModel.DataAnnotations;

public class AppFile{
    [Key]
    public Guid Id {get;set;}
    public string CaminhoDoArquivo {get;set;}
    public string Token {get;set;}
}