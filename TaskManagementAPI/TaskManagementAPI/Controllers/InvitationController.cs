using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.InputValidation;
using TaskManagementBusinessLayer.Invitations;
using TaskManagementBusinessLayer.Teams.TeamMembers;
using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.InvitationData;


namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitation _invitation;
        private readonly ITeamMember _teamMember;
        private readonly ILogger<InvitationController> _logger;
        private readonly IValidateInput _validateInput;

        public InvitationController(IInvitation invitation,ITeamMember teamMember,ILogger<InvitationController>logger,IValidateInput validateInput)
        {
            this._invitation = invitation;
            this._teamMember = teamMember;
            this._logger = logger;
            this._validateInput = validateInput;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetAllMyInvitations")]
        public async Task<ActionResult<IEnumerable<InvitationDTO>>> GetAllMyInvitations()
        {
            _logger.LogInformation("GET api/GetAllMyInvitations");
            var invitationlist = await _invitation.GetAllMyInvitations();
            if (invitationlist != null)
                return Ok(invitationlist);
            throw new BadRequestException("the GET call to api/GetAllMyInvitations failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("InviteNewMember")]
        public async Task<ActionResult<InvitationDTO>> InviteNewMember([FromBody]InvitationBaseModel newInvitation)
        {
            _logger.LogInformation("POST api/InviteNewMember");
            if(!_validateInput.InvitationDataValidator(newInvitation))
                throw new BadRequestException("The POST call to api/InviteNewNumber failled due to Input Validation!");

            if (!await _teamMember.IsMainTeamMember(newInvitation.team_id))
                throw new ForbiddenRequestException("The Current User is not Team Member or Doesn't have Rights to Invite user!");

            InvitationDTO newInvitationDTO = await _invitation.InviteNewTeamMember(
                new InvitationDTO(newInvitation.invitation_id,
                newInvitation.sent_by,
                newInvitation.received_by,
                newInvitation.team_id,
                (Roles)newInvitation.role_id,
                newInvitation.sent_at,
                newInvitation.is_accepted));

            if (newInvitationDTO != null)
                return Ok(newInvitationDTO);
            throw new BadRequestException("The POST call to api/InviteNewNumber failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{invitationID}")]
        public async Task<IActionResult> DeleteInvitation(int invitationID)
        {
            _logger.LogInformation($"DELETE api/{invitationID}");
            if (await _invitation.DeleteInvitation(invitationID))
                return Ok($"Invitation with ID {invitationID} Has Been Deleted Successfully!");
            throw new BadRequestException($"the DELETE call to api/{invitationID}");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{invitationID}")]
        public async Task<IActionResult> AcceptInvitation(int invitationID)
        {
            _logger.LogInformation($"PUT api/{invitationID}");
            if (await _invitation.AcceptInvitation(invitationID))
                return Ok($"Invitation with ID {invitationID} was accepted!");
            throw new BadRequestException($"The PUT Call to api/{invitationID} failled!");
        }
    }
}
