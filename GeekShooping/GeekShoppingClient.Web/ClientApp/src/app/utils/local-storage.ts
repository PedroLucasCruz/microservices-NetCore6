
export class LocalStorageUtils {

    public usuarioLogado(): boolean{

        let token = this.obterTokenUsuario();

        return !token;

    }
    
    public obterUsuario() {
     const retorno = localStorage?.getItem('user');
        return JSON.parse(retorno ?? ''); //rever mais tarde
    }

    public salvarDadosLocaisUsuario(response: any) {
        this.salvarTokenUsuario(response.access_token);
        
    }

    public limparDadosLocaisUsuario() {
        localStorage.removeItem('token');       
    }

    public obterTokenUsuario(): string {
      const retorno = localStorage?.getItem('token');
     return  retorno ?? 'token'.toString(); //rever mais tarde    
    }

    public salvarTokenUsuario(token: string) {
        localStorage.setItem('token', token);
    }

    public salvarUsuario(user: string) {
        localStorage.setItem('ressarcimento.user', JSON.stringify(user));
    }
     //-------------PERFIL---------------------------------
     public salvarPerfil(tipo: string){
        localStorage.setItem('perfil.Selecionado', tipo);        
    }

    public  limparPerfil(){
        localStorage.removeItem('perfil.Selecionado');
    }

    public  obterPerfil(){
      return  localStorage.getItem('perfil.Selecionado');
    }

    //-----------------------------------------------------

    public salvarClaimLogin(value: string){
        localStorage.setItem('Claim.Login', value);   
    }

    public limparClaimLogin(){
        localStorage.removeItem('Claim.Login');
    }

    public atualizarClaimLogin(value: string){
        localStorage.removeItem('Claim.Login');
        localStorage.setItem('Claim.Login', value)
    }
    
    public obterClaimLogin(){
      return localStorage.getItem('Claim.Login');
    } 

}