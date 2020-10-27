import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';

import { Injectable } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class MemberDetailedResolver implements Resolve<Member> {

    constructor(private memberService: MembersService) {}
        
    resolve(route: ActivatedRouteSnapshot): Member | Observable<Member> {
        return this.memberService.getMember(route.paramMap.get('username'));
    }
    
}

