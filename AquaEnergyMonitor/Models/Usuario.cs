using AquaEnergyMonitor.Models.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AquaEnergyMonitor.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; } = string.Empty;

        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
        [NotMapped]
        public string ConfirmaSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Endereço é obrigatório")]
        public string Endereco { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cidade é obrigatório")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado é obrigatório")]
        [MaxLength(2, ErrorMessage = "Adicione apenas a sigla do estado. Exemplo: SP")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório")]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessage = "Perfil é obrigatório")]
        [Range(1, 2, ErrorMessage = "Selecione um tipo de perfil válido")]
        public TipoPerfilEnum Perfil { get; set; } = TipoPerfilEnum.Residencial; // Adicione esse valor

        [Required(AllowEmptyStrings = false, ErrorMessage = "Quantidade de pessoas é obrigatório")]
        [Range(1, 100, ErrorMessage = "Informe um valor maior que zero")]
        public int? QuantPessoas { get; set; } = 1;

        public ICollection<ConsumoAgua> ConsumoAgua { get; set; }
        public ICollection<ConsumoEnergia> consumoEnergia { get; set; }
    }
}
