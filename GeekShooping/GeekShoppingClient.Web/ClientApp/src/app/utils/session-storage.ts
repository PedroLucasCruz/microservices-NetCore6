export class SessionStorageUtils {
    //----------------PERFIL SESSION------------------------------------------
    public salvarPefilSelecionado(value: string) {
        sessionStorage.setItem('perfil.selecionado', value);
    }

    public limparPerfilSelecionar() {
        sessionStorage.removeItem('perfil.selecionado');
    }

    public atualizarPerfilSelecionado(value: string) {
        sessionStorage.removeItem('perfil.selecionado');
        sessionStorage.setItem('perfil.selecionado', value)
    }

    public obterPerfil() {
        return sessionStorage.getItem('perfil.Selecionado');
    }
    //--------------Claim.Login-----------------------------------------------

    public salvarClaimDados(value: any) {
        sessionStorage.setItem('claim.dados', value);
    }

    public limparClaimDados() {
        sessionStorage.removeItem('claim.dados');
    }

    public atualizarClaimDados(value: string) {
        sessionStorage.removeItem('claim.dados');
        sessionStorage.setItem('claim.dados', value)
    }

    public obterClaimDados() {
        return sessionStorage.getItem('claim.dados');
    }
   
}